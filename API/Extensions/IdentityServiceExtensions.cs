using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<AppUser>();

            builder = new Microsoft.AspNetCore.Identity.IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication();
            
            return services;
        }
    }
}
