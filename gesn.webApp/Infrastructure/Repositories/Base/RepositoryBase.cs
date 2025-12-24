using Dapper;
using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Interfaces.Data;
using gesn.webApp.Interfaces.Repositories.Base;
using gesn.webApp.Models.Enums.Global;
using gesn.webApp.Models.Enums.Template;
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

        public async Task<IEnumerable<T>> ReadAsync(QueryTemplate? template = null, IList<WhereTemplate> whereAdicional = default, object? parametros = null)
        {
            IEnumerable<T>? obj = default;
            var builder = new SqlBuilder().Select($@"SELECT {template?.Select ?? "*"} FROM {this._tableName} {this._tableName.Substring(0, 1)}");
            SetJoins(template, builder);
            SetWhere(template, builder);
            SetAdditionalWhere(whereAdicional, builder);

            if (!string.IsNullOrWhiteSpace(template?.OrderBy))
                builder.OrderBy(template.OrderBy);

            if (!string.IsNullOrWhiteSpace(template?.Having))
                builder.Having(template.Having);

            if (!string.IsNullOrWhiteSpace(template?.GroupBy))
                builder.GroupBy(template.GroupBy);

            builder.AddParameters(parametros ?? new { });

            Template templateSql = builder.AddTemplate("/**select**/ /**from**/ /**innerjoin**/ /**leftjoin**/ /**rightjoin**/ /**where**/ /**groupby**/ /**having**/ /**order_by**/");

            if (null != _connectionFactory)
            {
                using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();

                if (null != connection)
                    obj = await connection.QueryAsync<T>(templateSql.RawSql);
            }

            return obj;
        }

        private static void SetAdditionalWhere(IList<WhereTemplate> whereAdicional, SqlBuilder builder)
        {
            if (null != whereAdicional && whereAdicional.Any())
            {
                foreach (var item in whereAdicional)
                {
                    switch (item.Type)
                    {
                        case EWhereType.OR:
                            builder.OrWhere(item.Statement);
                            break;
                        case EWhereType.AND:
                        default:
                            builder.OrWhere(item.Statement);
                            break;
                    }
                }
            }
        }

        private static void SetWhere(QueryTemplate? template, SqlBuilder builder)
        {
            if (template != null && template.Wheres.Any())
            {
                foreach (var item in template.Wheres)
                {
                    switch (item.Type)
                    {
                        case EWhereType.OR:
                            builder.OrWhere(item.Statement);
                            break;
                        case EWhereType.AND:
                        default:
                            builder.Where(item.Statement);
                            break;
                    }
                }
            }
        }

        private static void SetJoins(QueryTemplate? template, SqlBuilder builder)
        {
            if (null != template && template.Joins.Any())
            {
                foreach (JoinTemplate join in template.Joins)
                {
                    switch (join.Type)
                    {
                        case EJoinType.INNER:
                            builder.InnerJoin(join.Statement);
                            break;
                        case EJoinType.LEFT:
                            builder.LeftJoin(join.Statement);
                            break;
                        case EJoinType.RIGHT:
                            builder.RightJoin(join.Statement);
                            break;
                    }
                }
            }
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
            PropertyInfo pk = PrimaryKeyProperty<T>();
            Guid newId = Guid.NewGuid();
            pk.SetValue(entity, newId.ToString());

            var props = GetInsertProperties<T>(entity);  // inclui Id, ignora nulos

            string columns = string.Join(", ", props.Select(p => p.name));
            string parameters = string.Join(", ", props.Select(p => $"@{p.parameter}"));

            string query = $"INSERT INTO {this._tableName} ({columns}) VALUES ({parameters});";
            if (null != this._connectionFactory)
            {
                using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
                await connection.ExecuteAsync(query, entity, transaction);
            }

            return newId;

            //Guid newId = Guid.NewGuid();
            //var builder = new SqlBuilder();
            //var props = GetInsertProperties<T>(entity);

            //var pk = PrimaryKeyProperty<T>();
            //pk.SetValue(entity, newId);
            //var pkColumnName = pk.Name;

            //var columns = string.Join(", ", props.Select(p => p.name));
            //var parameters = string.Join(", ", props.Select(p => $"@{p.parameter}"));

            //string query = $@"INSERT INTO {this._tableName} ({columns}) VALUES ({parameters})";

            //if (HasIdentity<T>())
            //{
            //    if (null != this._connectionFactory)
            //    {
            //        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();

            //        if (null != connection)
            //            newId = await connection.ExecuteScalarAsync<Guid>(query, entity, transaction);

            //        if (Guid.Empty != newId)
            //            SetProperty<T>(entity, PrimaryKeyName<T>(), newId);
            //    }
            //}

            //return newId;
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

        private static List<(string name, string parameter)> GetInsertProperties<T>(T entity)
        {
            return typeof(T).GetProperties()
                .Where(p => p.CanRead && p.GetValue(entity) != null)
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