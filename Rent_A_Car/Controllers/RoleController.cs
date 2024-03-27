using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Rent_A_Car.ViewModels;
using Rent_A_Car.Areas.Identity.Data;
using Rent_A_Car.Data;

namespace Rent_A_Car.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly AuthDbContext authDbContext;

        public RoleController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AuthDbContext authDbContext) 
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.authDbContext = authDbContext;
        }

        [HttpGet]
        public IActionResult AddUserToRole()
        {
            return View();
        }

        [HttpPost]
        public async Task AddUserToRole(AddUserToRoleViewModel model)
        {
            User user = await userManager.FindByNameAsync(model.User);
            await userManager.AddToRoleAsync(user, model.Role);
        }

        [HttpGet]
        public IActionResult ListRole()
        {
            var a = roleManager.Roles;
            List<string> roles = a
                .Select(role => role.Name)
                .ToList();
            return View(roles);
        }


        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName  
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if(result.Succeeded)
                {
                    RedirectToAction("ListRole", "Role");
                }

                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByNameAsync(model.ExistingRoleName);

            if (role == null)
            {
                return NotFound();
            }

            role.Name = model.RoleName;
            var result = await roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("ListRole", "Role");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View("Error"); 
            }
        }

        [HttpGet]
        public IActionResult DeleteRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(DeleteRoleViewModel model)
        {
            var role = await roleManager.FindByNameAsync(model.ExistingRoleName);

            if (role == null)
            {
                return NotFound();
            }

            var result = await roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("ListRole", "Role");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View("Error");
            }
        }
    }
}
