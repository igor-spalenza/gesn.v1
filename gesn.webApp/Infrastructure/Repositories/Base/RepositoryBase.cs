using Dapper;
using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Interfaces.Data;
using gesn.webApp.Interfaces.Repositories.Base;
using gesn.webApp.Models.Enums.Global;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using static Dapper.SqlBuilder;

namespace gesn.webApp.Data.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly string _tableName = typeof(T).Name;

        public RepositoryBase(IDbConnectionFactory conn)
        {
            _connectionFactory = conn;
        }

        public virtual async Task<bool> UpdateAsync(T entity/*, bool ignoreNulls = true, params string[] extraIgnoreProperties*/)
        {
            bool flagUpdated = false;

            try
            {

            }
            catch (Exception error)
            {
                throw error;
            }

            return flagUpdated;
        }

        public virtual async Task<T> GetAsync(Guid id)
        {
            T? obj = default;
            string query = $@"SELECT * FROM {this._tableName} WHERE Id = @Id";

            if (null != _connectionFactory)
            {
                using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();

                if (null != connection && !Guid.Empty.Equals(id))
                    obj = await connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
            }

            return obj;
        }

        public async Task<IEnumerable<T>> ReadAsync(QueryTemplate? template = null, string? whereAdicional = null, object? parametros = null)
        {
            IEnumerable<T>? obj = default;
            var builder = new SqlBuilder().Select(template?.Select ?? "*");

            if (!string.IsNullOrWhiteSpace(template?.Joins))
                builder.Join(template.Joins);

            if (!string.IsNullOrWhiteSpace(template?.Where))
                builder.Where(template.Where);

            if (!string.IsNullOrWhiteSpace(whereAdicional))
                builder.Where(whereAdicional);

            if (!string.IsNullOrWhiteSpace(template?.OrderBy))
                builder.OrderBy(template.OrderBy);

            builder.AddParameters(parametros ?? new { });

            Template templateSql = builder.AddTemplate("/**select**/ /**from**/ /**where**/ /**order_by**/");

            if (null != _connectionFactory)
            {
                using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();

                if (null != connection)
                    obj = await connection.QueryAsync<T>(templateSql.RawSql);
            }

            return obj;
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            IList<T>? obj = default;
            string query = $@"SELECT * FROM {this._tableName}";

            if (null != _connectionFactory)
            {
                using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();

                if (null != connection)
                {
                    var result = await connection.QueryAsync<T>(query);
                    obj = result.ToList();
                }
            }

            return obj;
        }

        public virtual async Task<Guid> AddAsync(T entity, IDbTransaction? transaction = null)
        {
            Guid newId = Guid.Empty;
            var builder = new SqlBuilder();
            var props = GetInsertProperties<T>();

            var pk = PrimaryKeyProperty<T>();
            var pkColumnName = pk.Name;

            var columns = string.Join(", ", props.Select(p => p.name));
            var parameters = string.Join(", ", props.Select(p => $"@{p.parameter}"));

            string query = $@"DECLARE @InsertedIds TABLE ({pkColumnName} UNIQUEIDENTIFIER
                              INSERT INTO {this._tableName} ({columns}) VALUES ({parameters})
                              SELECT {pkColumnName} FROM @InsertedIds;";

            if (HasIdentity<T>())
            {
                if (null != this._connectionFactory)
                {
                    using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();

                    if (null != connection)
                        newId = await connection.ExecuteScalarAsync<Guid>(query, entity, transaction);

                    if (Guid.Empty != newId)
                        SetProperty<T>(entity, PrimaryKeyName<T>(), newId);
                }
            }

            return newId;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            bool flagDeleted = false;
            string query = $@"UPDATE {_tableName} SET StateCode = @StateCode WHERE Id = @Id";

            if (null != _connectionFactory)
            {
                using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();

                if (null != connection)
                    flagDeleted = await connection.ExecuteAsync(query, new { StateCode = (int)EObjectState.INACTIVE, Id = id }) > 0;
            }

            return flagDeleted;
        }

        private static List<(string name, string parameter)> GetUpdateProperties<T>(
            bool ignoreNulls, string[] extraIgnore)
        {
            var ignore = new HashSet<string>(extraIgnore.Concat(new[] { "Id", "CreatedAt", "CreatedBy" }));

            return typeof(T).GetProperties()
                .Where(p => !ignore.Contains(p.Name) && p.CanRead && p.CanWrite)
                .Where(p => !ignoreNulls || p.GetValue(Activator.CreateInstance<T>()) != null)
                .Select(p => (name: p.Name, parameter: p.Name))
                .ToList();
        }

        private static List<(string name, string parameter)> GetInsertProperties<T>()
        {
            return typeof(T).GetProperties()
                .Where(p => p.Name != "Id" && p.CanRead)
                .Select(p => (name: p.Name, parameter: p.Name))
                .ToList();
        }

        private static string TableName<T>() => $@"{typeof(T).Name}s";

        private static string PrimaryKeyName<T>() => "Id";

        private static bool HasIdentity<T>() => typeof(T).GetProperty("Id") != null;

        private static void SetProperty<T>(T entity, string propName, object value) =>
            typeof(T).GetProperty(propName)?.SetValue(entity, value);

        public static PropertyInfo PrimaryKeyProperty<T>()
        {
            return typeof(T).GetProperties()
                .FirstOrDefault(p =>
                    p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.Equals($"{typeof(T).Name}Id", StringComparison.OrdinalIgnoreCase) ||
                    p.GetCustomAttributes(typeof(KeyAttribute), true).Any()
                ) ?? throw new Exception($"PK não encontrada em {typeof(T).Name}");
        }
    }
}