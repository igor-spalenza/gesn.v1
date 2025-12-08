using System.Data;

namespace gesn.webApp.Interfaces.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
        Task<IDbConnection> CreateConnectionAsync();
    }
}
