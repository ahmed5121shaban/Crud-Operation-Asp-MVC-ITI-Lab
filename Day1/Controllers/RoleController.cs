using Maneger;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using ModelView;

namespace Day1.Controllers
{
    public class RoleController : Controller
    {
        ManagerRole roleManager;
        public RoleController(ManagerRole _roleManager)
        {
            roleManager = _roleManager;
        }

        [Authorize(Roles="Admin")]
        [HttpGet]
        public IActionResult AddRole()
        {
            ViewBag.roles = roleManager.GetAll().ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }

             var res =await roleManager.Add(role);
            if (!res.Succeeded)
            {
                ModelState.AddModelError("", "You Have Error In Your Addding");
            }
            return View();
        }
    }
}
