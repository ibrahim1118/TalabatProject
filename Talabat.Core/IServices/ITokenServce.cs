using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entitys.Identity;

namespace Talabat.Core.IServices
{
    public interface ITokenServce
    {

        public Task<string> CreatTokeAysnc(AppUser user, UserManager<AppUser> userManager); 
    }
}
