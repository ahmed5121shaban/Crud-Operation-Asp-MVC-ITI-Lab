using Microsoft.AspNetCore.Mvc;

namespace Day1.Controllers
{
    public class AcountController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
