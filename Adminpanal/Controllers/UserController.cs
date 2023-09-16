using Adminpanal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Stripe;
using Talabat.Core.Entitys.Identity;

namespace Adminpanal.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.Users.Select(u => new UserVm
            {
                Id  = u.Id,
                DispalyName =u.DisplayName,
                Email=u.Email,
                PhoneNumber=u.PhoneNumber,
                UserName = u.UserName,
                Roles = userManager.GetRolesAsync(u).Result
            }).ToListAsync();
            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id== null)
            {
              return BadRequest();
            }
            var user = await userManager.FindByIdAsync(id);

            if (user == null) {
              return NotFound();
            }

            var userRole = new UserRoleVM()
            { 
               UserId= user.Id,
               UserName= user.UserName, 
               Roles = roleManager.Roles.Select(r=> new RoleViewModle
               {
                   Id = r.Id,
                   Name= r.Name,
                   isSelected = userManager.IsInRoleAsync(user , r.Name).Result
               }).ToList()
            };
            
            return View(userRole);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleVM userRoleVM)
        {
             var user = await userManager.FindByIdAsync(userRoleVM.UserId);
             var userRole = await userManager.GetRolesAsync(user);
            foreach (var role in userRoleVM.Roles)
            {
                if (userRole.Any(r => r == role.Name) && !role.isSelected)
                {
                    await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                if (!userRole.Any(r => r == role.Name) && role.isSelected)
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }
            }
            return RedirectToAction("Index");
          
        }
    }
}
