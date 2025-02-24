﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser?>FindUserWithAddressByEmailAsync(this UserManager<AppUser> usermanager,ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await usermanager.Users.Include(a => a.Address).FirstOrDefaultAsync(U => U.Email == email);
            return user;
        }
    }
}
