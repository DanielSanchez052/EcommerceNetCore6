using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            //conection to identity database
            services.AddDbContext<AppIdentityDbContext>(o => {
                o.UseSqlServer(config.GetConnectionString("IdentityConnection"));
            });

            //Identity Configuration
            var builder = services.AddIdentityCore<AppUser>();

            builder = new Microsoft.AspNetCore.Identity.IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();


            services.AddAuthentication();
            
            return services;
        }
    }
}
