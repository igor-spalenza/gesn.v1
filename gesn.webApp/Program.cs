using gesn.webApp.Data;
using gesn.webApp.Data.Migrations;
using gesn.webApp.Infrastructure.Configuration;
using gesn.webApp.Infrastructure.Middleware;
using gesn.webApp.Interfaces.Data;
using GesN.Web.Data.Migrations;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
    builder.Configuration.AddUserSecrets<Program>();
else
    builder.Configuration.AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

SQLitePCL.Batteries.Init();
string dbPath = Path.Combine(AppContext.BaseDirectory, "/GesN.Web/Data/Database/gesn.db");

builder.Services.AddIdentityServices();
builder.Services.AddAuthenticationServices();
builder.Services.AddAuthorizationServices();

// Configurar antiforgery para aceitar headers (necessário para AJAX com JSON)
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken";
});

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructureServices(connectionString);

builder.Services.AddGoogleWorkspaceConfiguration(builder.Configuration);

try
{
    // Verificar se o arquivo do banco existe
    string dbPath2 = connectionString.Replace("Data Source=", "").Split(';')[0];
    if (!File.Exists(dbPath2))
    {
        Console.WriteLine("Banco não existe. Criando...");
        using (var scope = builder.Services.BuildServiceProvider().CreateScope())
        {
            var identityInit = new IdentitySchemaInit(connectionString);
            identityInit.Initialize();

            var dbInit = new DbInit(connectionString);
            dbInit.Initialize();

            var connectionFactory = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();
            var seedData = new SeedData(connectionFactory);
            await seedData.Initialize();
            Console.WriteLine("Banco criado com sucesso!");
        }
    }
    else
    {
        Console.WriteLine("Banco já existe. Pulando inicialização.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao inicializar banco: {ex.Message}");
}

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseLoginRateLimiting();
app.UseAuthorization();

// Mapear a rota da área Admin primeiro
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Depois a rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();