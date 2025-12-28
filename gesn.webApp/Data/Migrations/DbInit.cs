using FluentMigrator.Runner;
using System.Reflection;

namespace gesn.webApp.Data.Migrations
{
    public class DbInit
    {
        private readonly string _connectionString;

        public DbInit(string connectionString) =>
            _connectionString = connectionString;

        public void Initialize()
        {
            IServiceProvider provider = CreateServices(_connectionString);
            IMigrationRunner runner = provider.GetRequiredService<IMigrationRunner>();
            runner.ListMigrations();
            runner.MigrateUp();
        }

        private static IServiceProvider CreateServices(string connectionString)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(config => config
                    .AddSQLite()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(config => config.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    }
}
