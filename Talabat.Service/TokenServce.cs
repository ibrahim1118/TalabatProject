using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Talabat.Core.Entitys.Identity;
using Talabat.Core.IServices;

namespace Talabat.Service
{
    public class TokenServce : ITokenServce
    {
        private readonly IConfiguration configuration;

        public TokenServce(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreatTokeAysnc(AppUser user, UserManager<AppUser> userManager)
        {
            var authClims = new List<Claim>()
            { 
                new Claim (ClaimTypes.GivenName , user.DisplayName), 
                new Claim (ClaimTypes.Email, user.Email),
            };
            var rols = await userManager.GetRolesAsync(user);
            foreach (var r in rols) {
                authClims.Add(new Claim(ClaimTypes.Role, r)); 
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issure"],
                audience: configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationTime"])),
                claims :authClims, 
                signingCredentials: new SigningCredentials (key , SecurityAlgorithms.HmacSha256Signature)
                );
           return  new  JwtSecurityTokenHandler().WriteToken(token);   
        }
    }
}
