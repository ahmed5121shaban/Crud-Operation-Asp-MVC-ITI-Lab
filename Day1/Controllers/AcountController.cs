using Day1.Helper;
using Maneger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelView;
using System.Security.Claims;

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

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserChangePassword viewmodel)
        {
            if (ModelState.IsValid)
            {
                viewmodel.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var res = await AcountManager.ChangePassword(viewmodel);
                if (res.Succeeded)
                {
                    return RedirectToAction("login");
                }
                else
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(viewmodel);
                }
            }
            return View(viewmodel);

        }




        [HttpGet]
        public IActionResult GetResetPasswordCode()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string email)
        {
            var code = await AcountManager.GetResetPasswordCode(email);
            if (string.IsNullOrEmpty(code))
            {
                ModelState.AddModelError("", "Your Email Is Not Here");
                return View("GetResetPasswordCode");
            }
            else
            {
                EmailHelper mail = new EmailHelper
                    (email, "Your Code is" , $" Code :  {code}");
                mail.Send();
                return View( new UserResetPasswordViewModel() { Email = email });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(UserResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var res = await AcountManager.ResetPassword(viewModel);
                if (res.Succeeded)
                {
                    return RedirectToAction("login","Acount");
                }
                else
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(viewModel);
        }
    }
}
