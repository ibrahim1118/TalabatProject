using Adminpanal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;

namespace Adminpanal.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync(); 
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleVM roleVM)
        {
            if (ModelState.IsValid) {
                var role = await roleManager.RoleExistsAsync(roleVM.Name);
                if (!role)
                    await roleManager.CreateAsync(new IdentityRole { Name = roleVM.Name });
                else
                {
                    ModelState.AddModelError("Name", "Role is exists");
                    RedirectToAction("Index");
                }
               return RedirectToAction("Index");
            }
            ModelState.AddModelError("Name", "The role Name is reqired"); 
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return BadRequest(); 
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            var rolevM = new RoleViewModle()
            {
                Name = role.Name,
                Id = role.Id,
            }; 
            return View(rolevM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModle rolevm)
        {
            if (ModelState.IsValid) { 
                var role = await roleManager.FindByIdAsync(rolevm.Id);
            role.Name = rolevm.Name;
            await roleManager.UpdateAsync(role);
            return RedirectToAction(nameof(Index));
            }
            return View(rolevm); 
        }
    }


}
