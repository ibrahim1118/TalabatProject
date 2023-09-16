using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entitys.Identity;

namespace Talabat.Repositiry.Identity
{
    public static class IdentityDbContextSeed
    {
        public static async Task SeedData(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Ibrahim Mohamed",
                    Email = "ib1118200@gmail.com",
                    UserName = "Ibrahim",
                    PhoneNumber = "011382323"
                };
                await userManager.CreateAsync(user, "123Asd@");
            }
        }
    }
}
