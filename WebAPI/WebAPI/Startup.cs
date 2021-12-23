using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Data;
using Data.Repository;

using Core;
using Core.Domain.Auth;
using Core.Domain.Cart;
using Core.Domain.Categories;
using Core.Domain.Orders;
using Core.Domain.Products;
using Core.Domain.Profiles;
using Core.Domain.Shops;
using Core.Handlers.Hashing;
using Core.Handlers.Emails;
using Core.Handlers.Template;
using Core.Handlers.Logging;

using WebAPI.Models.Settings;

using WebAPI.API.MailKit;
using WebAPI.API.RazorTemplateEngine;
using WebAPI.API.Serilog;
using WebAPI.API.Common;

namespace WebAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
            => this._configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
           => services
                .Configure<AuthOptions>(this._configuration.GetSection("Auth"))
                .Apply(this.ConfigureCORS)
                .Apply(this.ConfigureAuthentication)
                .Apply(this.RegisterSettings)
                .Apply(this.RegisterHandlers)
                .Apply(this.RegisterRepositories)
                .Apply(this.RegisterServices)
                .AddControllersWithViews();

        public void Configure(IApplicationBuilder app)
         => app
            .UseRouting()
            .UseCors()
            .Apply((builder) => SerilogConfigurator.Add(builder, this._configuration))
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints.MapControllers());

        private IServiceCollection ConfigureCORS(IServiceCollection services)
            => services
                .AddCors(options => options
                    .AddDefaultPolicy(corsPolicyBuilder => corsPolicyBuilder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("Content-Disposition")));

        private IServiceCollection ConfigureAuthentication(IServiceCollection services)
            => services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = this._configuration.GetValue<string>("Auth:Issuer"),
                        ValidateAudience = true,
                        ValidAudience = this._configuration.GetValue<string>("Auth:Audience"),
                        ValidateLifetime = true,
                        IssuerSigningKey = this._configuration.GetSection("Auth").Get<AuthOptions>().GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                })
                .Bind(services);

        private IServiceCollection RegisterSettings(IServiceCollection services)
            => services
                .AddSingleton<IDbSettings>(new DbSettings { ConnectionString = this._configuration.GetConnectionString("ProductAppCon") })
                .AddSingleton<IHashingSettings>(this._configuration.GetSection("HashingSettings").Get<HashingSettings>())
                .AddSingleton<IMailKitSettings>(this._configuration.GetSection("EmailConfiguration").Get<MailKitSettings>());

        private IServiceCollection RegisterHandlers(IServiceCollection services)
            => services
                .AddScoped<IEmailSmtpClient, EmailStmpClient>()
                .AddScoped<IRazorTemplate, RazorTemplate>()
                .AddScoped<IEmailNotificationService, EmailNotificationService>()
                .AddScoped<ILogger, SerilogLogger>();

        private IServiceCollection RegisterRepositories(IServiceCollection services)
            => services
                .AddScoped<IAuthRepository, AuthRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IShopRepository, ShopRepository>()
                .AddScoped<IProfileRepository, ProfileRepository>();

        private IServiceCollection RegisterServices(IServiceCollection services)
            => services
                .AddScoped<HashingService, HashingService>()
                .AddScoped<ProductServices, ProductServices>()
                .AddScoped<OrderServices, OrderServices>()
                .AddScoped<CategoryServices, CategoryServices>()
                .AddScoped<AuthServices, AuthServices>()
                .AddScoped<CartServices, CartServices>()
                .AddScoped<ShopServices, ShopServices>()
                .AddScoped<ProfileServices, ProfileServices>();
    }
}