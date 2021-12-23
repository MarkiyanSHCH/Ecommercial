using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using WebAPI.API.Common;
using WebAPI.API.Serilog;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateHostBuilder(args)
                .Apply(SerilogConfigurator.Use)
                .Build()
                .Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}