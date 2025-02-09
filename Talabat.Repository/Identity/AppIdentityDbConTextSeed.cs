using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
   public class AppIdentityDbConTextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any()) {

                var user = new AppUser()
                {
                    DisplayName = "Ali AboRashed",
                    Email = "aliaborashed31@gmail.com",
                    UserName = "Ali.AboReahed",
                    PhoneNumber = "01202662188"
                };
                await userManager.CreateAsync(user,"246810Ali#");
            
            }
        }


    }
}
