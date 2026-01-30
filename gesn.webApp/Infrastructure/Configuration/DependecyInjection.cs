using Dapper;
using gesn.webApp.Areas.Identity.Data.Models;
using gesn.webApp.Areas.Identity.Data.Stores;
using gesn.webApp.Data.Migrations;
using gesn.webApp.Infrastructure.Repositories.Global;
using gesn.webApp.Infrastructure.Repositories.Offer;
using gesn.webApp.Infrastructure.Services;
using gesn.webApp.Infrastructure.Services.Global;
using gesn.webApp.Infrastructure.Services.Offer;
using gesn.webApp.Interfaces.Data;
using gesn.webApp.Interfaces.Repositories.Global;
using gesn.webApp.Interfaces.Repositories.Offer;
using gesn.webApp.Interfaces.Services.Global;
using gesn.webApp.Interfaces.Services.Offer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace gesn.webApp.Infrastructure.Configuration
{
    public static class DependecyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbConnectionFactory>(provider => new ProjectDataContext(connectionString));

            services.AddHttpContextAccessor();

            //Offer
            services.AddScoped<IOfferService, OfferServices>();
            services.AddScoped<IOfferRepository, OfferRepository>();

            //Global
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITypeService, TypeServices>();
            services.AddScoped<ITypeRepository, TypeRepository>();

            // Google Workspace Integration (moved to Integration area)
            //services.AddScoped<IGooglePeopleService, GooglePeopleService>();

            //// Domínio Sales
            //services.AddScoped<ICustomerRepository, CustomerRepository>();
            //services.AddScoped<ICustomerService, CustomerService>();
            //services.AddScoped<IContractRepository, ContractRepository>();
            //services.AddScoped<IContractService, ContractService>();
            //services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            //// Domínio Production
            //services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            //services.AddScoped<IProductCategoryService, ProductCategoryService>();
            //services.AddScoped<ISupplierRepository, SupplierRepository>();
            //services.AddScoped<ISupplierService, SupplierService>();
            //services.AddScoped<IIngredientRepository, IngredientRepository>();
            //services.AddScoped<IIngredientService, IngredientService>();
            //services.AddScoped<IProductIngredientRepository, ProductIngredientRepository>();
            //services.AddScoped<IProductIngredientService, ProductIngredientService>();

            //// Sprint 4 - Advanced Production Entities
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IProductComponentRepository, ProductComponentRepository>();
            //services.AddScoped<IProductComponentService, ProductComponentService>();
            //services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
            //services.AddScoped<IProductGroupItemRepository, ProductGroupItemRepository>();
            //services.AddScoped<IProductGroupItemService, ProductGroupItemService>();
            //services.AddScoped<IProductGroupExchangeRuleRepository, ProductGroupExchangeRuleRepository>();
            //services.AddScoped<IProductGroupExchangeRuleService, ProductGroupExchangeRuleService>();
            //services.AddScoped<IProductGroupService, ProductGroupService>();
            //services.AddScoped<IOrderItemService, OrderItemService>();
            //services.AddScoped<IProductionOrderRepository, ProductionOrderRepository>();
            //services.AddScoped<IProductionOrderService, ProductionOrderService>();

            //// Sprint 4 - Composite Product Support (Demand & Hierarchy)
            //services.AddScoped<IDemandRepository, DemandRepository>();
            //services.AddScoped<IDemandService, DemandService>();
            //services.AddScoped<IProductComponentHierarchyRepository, ProductComponentHierarchyRepository>();
            //services.AddScoped<IProductComponentHierarchyService, ProductComponentHierarchyService>();
            //services.AddScoped<ICompositeProductXHierarchyRepository, CompositeProductXHierarchyRepository>();
            //services.AddScoped<ICompositeProductXHierarchyService, CompositeProductXHierarchyService>();

            //// Legados
            //services.AddScoped<IClienteService, ClienteService>();
            //services.AddScoped<IClienteRepository, ClienteRepository>();
            //services.AddScoped<IPedidoService, PedidoService>();
            //services.AddScoped<IPedidoRepository, PedidoRepository>();
            //services.AddScoped<IProductRepositoryV2, ProductRepositoryV2>();

            services.AddMemoryCache();
        }

        public static void AddGoogleWorkspaceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurar Google Workspace settings
            //services.Configure<GoogleWorkspaceSettings>(
            //    configuration.GetSection(GoogleWorkspaceSettings.SectionName));
        }

        public async static void EnsureDatabaseInitialized(this IServiceProvider serviceProvider, string connectionString)
        {
            var dbInit = new DbInit(connectionString);
            dbInit.Initialize();

            DefaultTypeMap.MatchNamesWithUnderscores = false;
        }

        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // suas opções existentes
            })
            .AddDefaultTokenProviders();

            // Registro manual com injeção de dependência
            services.AddScoped<IUserStore<ApplicationUser>>(sp =>
                new DapperUserStore(sp.GetRequiredService<IDbConnectionFactory>()));
            services.AddScoped<IRoleStore<ApplicationRole>>(sp =>
                new DapperRoleStore(sp.GetRequiredService<IDbConnectionFactory>()));

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
                UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>();
            services.AddTransient<IEmailSender, EmailSender>();
            //services.AddScoped<ITestesServices, TesteServices>();
        }

        public static void AddAuthenticationServices(this IServiceCollection services)
        {
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/Identity/Account/Login";
            //    options.LogoutPath = "/Identity/Account/Logout";
            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";

            //    options.ExpireTimeSpan = TimeSpan.FromHours(8);
            //    options.SlidingExpiration = true;
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            //    options.Cookie.SameSite = SameSiteMode.Lax;

            //    options.Events.OnRedirectToLogin = context =>
            //    {
            //        if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            //        {
            //            context.Response.StatusCode = 401;
            //            return Task.CompletedTask;
            //        }
            //        context.Response.Redirect(context.RedirectUri);
            //        return Task.CompletedTask;
            //    };
            //});
            services.ConfigureApplicationCookie(opt => { 
                opt.LoginPath = "/Identity/Account/Login";
                opt.LogoutPath = "/Identity/Account/Logout";
                opt.AccessDeniedPath = "/Identity/Account/AccessDenied";
                opt.ExpireTimeSpan = TimeSpan.FromHours(8);
                opt.SlidingExpiration = true;
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.Cookie.SameSite = SameSiteMode.Lax;
                opt.Events.OnRedirectToLogin = context =>
                {
                    if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    }
                    context.Response.Redirect(context.RedirectUri);
                    return Task.CompletedTask;
                };
            });
            //services.AddAuthentication()
            //        .AddCookie(IdentityConstants.ApplicationScheme, options =>
            //        {
            //            options.LoginPath = "/Identity/Account/Login";
            //            options.LogoutPath = "/Identity/Account/Logout";
            //            options.AccessDeniedPath = "/Identity/Account/AccessDenied";

            //            options.ExpireTimeSpan = TimeSpan.FromHours(8);
            //            options.SlidingExpiration = true;
            //            options.Cookie.HttpOnly = true;
            //            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            //            options.Cookie.SameSite = SameSiteMode.Lax;

            //            options.Events.OnRedirectToLogin = context =>
            //            {
            //                if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            //                {
            //                    context.Response.StatusCode = 401;
            //                    return Task.CompletedTask;
            //                }
            //                context.Response.Redirect(context.RedirectUri);
            //                return Task.CompletedTask;
            //            };
            //        })
            //        .AddCookie(IdentityConstants.ExternalScheme);  // resolve o handler faltante
        }

        public static void AddAuthorizationServices(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("GerenciarUsuarios", policy =>
                    policy.RequireClaim("permissao", "usuarios:gerenciar"));

                options.AddPolicy("GerenciarClientes", policy =>
                    policy.RequireClaim("permissao", "clientes:gerenciar"));

                options.AddPolicy("GerenciarPedidos", policy =>
                    policy.RequireClaim("permissao", "pedidos:gerenciar"));
            });
        }
    }
}
