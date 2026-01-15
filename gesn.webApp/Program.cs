using gesn.webApp.Data;
using gesn.webApp.Data.Migrations;
using gesn.webApp.Infrastructure.Configuration;
using gesn.webApp.Infrastructure.FluentValidation;
using gesn.webApp.Infrastructure.Mappers;
using gesn.webApp.Infrastructure.Middleware;
using gesn.webApp.Interfaces.Data;
using FluentValidation.AspNetCore; // Adicione esta diretiva no topo do arquivo

var builder = WebApplication.CreateBuilder(args);
var connectionString = string.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}
else // RELEASE MODE 
{
    builder.Configuration.AddEnvironmentVariables();
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

SQLitePCL.Batteries.Init();
string dbPath = Path.Combine(AppContext.BaseDirectory, "/gesn.webApp/Data/gesn.db");

builder.Services.AddIdentityServices();
builder.Services.AddAuthenticationServices();
builder.Services.AddAuthorizationServices();
builder.Services.RegisterValidators();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken";
});

builder.Services.AddRazorPages();                                           // Razor Pages
builder.Services.AddControllersWithViews(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
}); 
builder.Services.AddInfrastructureServices(connectionString);               // DI + IoC Container | Configs Injection
builder.Services.RegisterMaps();                                            // Mapster Configuration
builder.Services.AddGoogleWorkspaceConfiguration(builder.Configuration);    // Google Workspace (People/Calendar/Maps) configs

var app = builder.Build();

try
{
    string dbPath2 = connectionString.Replace("Data Source=", "").Split(';')[0];
    using (var scope = app.Services.CreateScope())
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
catch (Exception ex)
{
    Console.WriteLine($"Erro ao inicializar banco: {ex.Message}");
}

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