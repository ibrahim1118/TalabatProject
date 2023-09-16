using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entitys.Identity;
using Talabat.Core.IServices;
using Talabat.Repositiry.Identity;
using Talabat.Service;

namespace Talabt.APIs.Extentions
{
    public static class AddIdentityServices
    {
        public static IServiceCollection AddIdentityService (this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped<ITokenServce, TokenServce>(); 

            services.AddIdentity<AppUser, IdentityRole>().
                AddEntityFrameworkStores<AppIdentityDbContext>();


            services.AddAuthentication(option=>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 option.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(option=>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:Issure"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:Audience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]))
                    };
                });

            return services;
        }
    }
}
