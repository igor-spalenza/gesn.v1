using Dapper;
using gesn.webApp.Data.Repositories.Base;
using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Interfaces.Data;
using gesn.webApp.Interfaces.Repositories.Offer;
using gesn.webApp.Models.Entities.Global;
using System.Data;
using static Dapper.SqlBuilder;
using OfferEntity = gesn.webApp.Models.Entities.Offer.Offer;

namespace gesn.webApp.Infrastructure.Repositories.Offer
{
    public class OfferRepository : RepositoryBase<OfferEntity>, IOfferRepository
    {
        //private readonly IDbConnectionFactory _connectionFactory;
        public OfferRepository(IDbConnectionFactory conn) : base(conn)
        {
            //this._connectionFactory = conn;
        }

        public override async Task<IEnumerable<OfferEntity>> ReadAsync(QueryTemplate? template = null, IEnumerable<WhereTemplate> whereAdicional = null, object? parametros = null)
        {
            IEnumerable<OfferEntity>? obj = default;
            var builder = new SqlBuilder().Select($@"SELECT {template?.Select ?? "o.*, c.*"} FROM {typeof(OfferEntity).Name} o");

            SetJoins(template, builder);
            SetWhere(template, builder);
            SetAdditionalWhere(whereAdicional, builder);

            if (null != parametros) builder.AddParameters(parametros);

            // 2. Geramos o template SQL
            Template templateSql = builder.AddTemplate("/**select**/ /**from**/ /**innerjoin**/ /**leftjoin**/ /**where**/ /**groupby**/ /**order_by**/");

            if (null != _connectionFactory)
            {
                using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();

                if (null != connection)
                    obj = await connection.QueryAsync<OfferEntity, Category, OfferEntity>(templateSql.RawSql, (offer, category) =>
                    {
                        offer.CategoryNavigation = category;
                        return offer;
                    },
                    param: templateSql.Parameters,
                    splitOn: "Id");
            }
            
            return obj;
        }
    }
}