using API.Extensions;
using API.Middleware;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//services
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();


var app = builder.Build();

// http request pipeline
app.UseMiddleware<ExcepcionMiddleware>();

app.UseRouting();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.UseSwaggerDocumentation();


app.UseHttpsRedirection();

app.MapControllers();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
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

app.Run();
