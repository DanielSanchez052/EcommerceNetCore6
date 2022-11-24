using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using (var score = host.Services.CreateScope())
        {
            var Services = score.ServiceProvider;
            var loggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = Services.GetRequiredService<StoreContext>();
                await context.Database.MigrateAsync();
                await new StoreContextSeed().SeedJsonAsync(context, loggerFactory);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during Migration");
            }
            host.Run();
        }

    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

