using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppidentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager){
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Adress =  new Address { 
                        FirstName = "Bob",
                        LastName = "Bobbity",
                        Street = "10 The Street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "90210"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");

            }
        }

    }
}
