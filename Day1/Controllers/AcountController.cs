using Maneger;
using Microsoft.AspNetCore.Mvc;
using ModelView;

namespace Day1.Controllers
{
    public class AcountController : Controller
    {
        AcountManager AcountManager { get; set; }

        public AcountController(AcountManager acountManager)
        {
            AcountManager = acountManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var res = await AcountManager.Login(user);
            if (res.Succeeded)
            {
                return RedirectToAction("index", "home");
            }

             ModelState.AddModelError("","The Email/User Name or Password is not Valid");
             return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            
            var res =await AcountManager.Register(user);
            if (res.Succeeded)
            {
                return RedirectToAction("login", "acount");
            }
            else
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(user);
            }

            
        }

        public IActionResult Logout()
        {
            AcountManager.LogOut();
            return RedirectToAction("index", "home");
        }
    }
}
