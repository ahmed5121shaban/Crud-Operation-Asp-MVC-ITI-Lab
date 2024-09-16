using Maneger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using ModelView;
using System.Data.Entity;

namespace Day1.Controllers
{
    public class RoleController : Controller
    {

        ManagerRole roleManager;
        public UserManager<User> UserManager { get; }
        public RoleController(ManagerRole _roleManager,UserManager<User> _userManager)
        {
            roleManager = _roleManager;
            UserManager = _userManager;
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

        [HttpPost]
        public async Task<IActionResult> AssignRole(string id ,string role)
        {
            var user = UserManager.Users.FirstOrDefault(x => x.Id == id);
            var res = await UserManager.AddToRoleAsync(user,role);
            if (res.Succeeded)
            {
                return RedirectToAction("index","home");
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public IActionResult AssignRole()
        {
            ViewBag.roles = roleManager.GetAll().ToList();
            ViewBag.users = UserManager.Users.ToList();
            return View();
        }
    }
}
