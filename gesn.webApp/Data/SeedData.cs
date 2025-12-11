using gesn.webApp.Data.Seeds.IdentitySeeds;
using gesn.webApp.Interfaces.Data;

namespace gesn.webApp.Data
{
    public class SeedData
    {
        private readonly IdentitySeeder _identitySeeder;

        public SeedData(IDbConnectionFactory connectionFactory)
        {
            _identitySeeder = new IdentitySeeder(connectionFactory);
        }

        public async Task Initialize()
        {
            try
            {
                await _identitySeeder.SeedAsync();
                Console.WriteLine("Identity seed completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during identity seed: {ex.Message}");
                throw;
            }
        }
    }
}
