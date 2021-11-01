using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Data.Repository;
using Core;
using Core.Services;
using Core.Repository;
using Core.Services.Hashing;
using WebAPI.Models;

namespace WebAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
            => _configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authOptions = _configuration.GetSection("Auth").Get<AuthOptions>();

            services.Configure<AuthOptions>(_configuration.GetSection("Auth"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {

                            ValidateIssuer = true,
                            ValidIssuer = authOptions.Issuer,


                            ValidateAudience = true,
                            ValidAudience = authOptions.Audience,

                            ValidateLifetime = true,


                            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllers();

            services.AddSingleton<IHashingSettings>(this._configuration.GetSection("HashingSettings").Get<HashingSettings>())
                    .AddScoped<IAuthRepository, AuthRepository>()
                    .AddScoped<IProductRepository, ProductRepository>()
                    .AddScoped<ICategoryRepository, CategoryRepository>()
                    .AddScoped<IOrderRepository, OrderRepository>()
                    .AddScoped<ICartRepository, CartRepository>()
                    .AddScoped<IShopRepository, ShopRepository>()
                    .AddScoped<HashingService, HashingService>()
                    .AddScoped<ProductServices, ProductServices>()
                    .AddScoped<OrderServices, OrderServices>()
                    .AddScoped<CategoryServices, CategoryServices>()
                    .AddScoped<AuthServices, AuthServices>()
                    .AddScoped<CartServices, CartServices>()
                    .AddScoped<ShopServices, ShopServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
