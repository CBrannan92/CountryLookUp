using Iso2CodeLookUp.Logic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Iso2CodeLookUp
{
    public class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHttpClient();
                   services.AddTransient<Application>();
               }).UseConsoleLifetime();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var myService = services.GetRequiredService<Application>();
                    await myService.Run();

                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Occured: {ex.Message}");
                }
            }


        }


    }
}

