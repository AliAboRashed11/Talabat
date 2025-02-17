﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Service;

namespace Talabat.Service
{
    public class TokenService : ITokenSevice
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
           
           _configuration = configuration;
        }

   

        public async Task<string> CreateTokenAsync(AppUser user,UserManager<AppUser> userManager)
        {
            var authClaims =new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,user.DisplayName),
                new Claim(ClaimTypes.GivenName,user.Email),
            };

            var userRols = await userManager.GetRolesAsync(user);

            foreach (var role in userRols) 
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            
                var autKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

                var token = new JwtSecurityToken(

                    issuer: _configuration["JWT:ValidIssuer"],
                    audience:_configuration["JWT:ValidAudience"],
                    expires:DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(autKey,SecurityAlgorithms.HmacSha256Signature)
                    
                    );
          return  new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
