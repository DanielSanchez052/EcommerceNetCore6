using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using (var score = host.Services.CreateScope())
        {
            var services = score.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                //seed data for products
                var context = services.GetRequiredService<StoreContext>();
                await context.Database.MigrateAsync();
                await new StoreContextSeed().SeedJsonAsync(context, loggerFactory);
                //seed data for identity
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityContext.Database.MigrateAsync();
                await AppidentityDbContextSeed.SeedUsersAsync(userManager);

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

