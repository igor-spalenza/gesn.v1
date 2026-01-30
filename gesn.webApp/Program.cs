using FluentValidation.AspNetCore;
using gesn.webApp.Data;
using gesn.webApp.Data.Migrations;
using gesn.webApp.Infrastructure.Configuration;
using gesn.webApp.Infrastructure.FluentValidation;
using gesn.webApp.Infrastructure.Mappers;
using gesn.webApp.Infrastructure.Middleware;
using gesn.webApp.Interfaces.Data;

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

builder.Services.AddInfrastructureServices(connectionString);               // DI + IoC Container | Configs Injection
builder.Services.RegisterMaps();                                            // Mapster Configuration
builder.Services.AddGoogleWorkspaceConfiguration(builder.Configuration);    // Google Workspace (People/Calendar/Maps) configs
builder.Services.AddLocalization(opt => opt.ResourcesPath = "Resources"); // Localization

builder.Services.AddControllersWithViews(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;

})
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            return factory.Create(type);
        };
    });


var app = builder.Build();

var supportedCultures = new[] { "en-US", "pt-BR" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures.Last())
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

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