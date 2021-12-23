using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace WebAPI.API.Serilog
{
    public static class SerilogConfigurator
    {
        public static IHostBuilder Use(IHostBuilder hostBuilder)
            => hostBuilder.UseSerilog();

        public static IApplicationBuilder Add(
            this IApplicationBuilder appBuilder,
            IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return appBuilder;
        }
    }
}
