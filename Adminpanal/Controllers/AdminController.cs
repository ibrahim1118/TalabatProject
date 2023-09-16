using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Talabat.Core.Entitys.Identity;
using Talabt.APIs.Dtos;

namespace Adminpanal.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AdminController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        
        public IActionResult Login()
        {
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto modle)
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByEmailAsync(modle.Email); 
                if (user == null) {
                    ModelState.AddModelError("Email", "In Valid Email"); 
                    return View(modle);
                }
                var res = await signInManager.CheckPasswordSignInAsync(user , modle.Password,false);
                if (!res.Succeeded||!await userManager.IsInRoleAsync(user , "Admin"))
                {
                    ModelState.AddModelError(string.Empty, "You Are Not Autherize"); 
                    return View(modle);
                }
                var res2 = await signInManager.PasswordSignInAsync(user, modle.Password, false, false); ;
                if (res2.Succeeded)
                    return RedirectToAction("Index", "Home");
                
            }
            return View(modle);  
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync(); 
            return RedirectToAction(nameof(Login));   
        }
    }
}
