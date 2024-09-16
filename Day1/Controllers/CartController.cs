using Maneger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Models;
using ModelView;
using System.Security.Claims;

namespace Day1.Controllers
{
    public class CartController : Controller
    {
        public UserManager<User> UserManager { get; }
        public CartManager cartManager { get; }
        public ProductManeger Product { get; }

        public CartController(UserManager<User> user, CartManager _cartManager, ProductManeger product)
        {
            UserManager = user;
            cartManager = _cartManager;
            Product = product;
        }

        public async Task<IActionResult> AddToCartPage()
        {
            var user = await UserManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var cart = cartManager.GetAllCart().Where(u => u.UserID == user.Id).ToList();
            return View(cart);
        }
        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await UserManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ProductViewModel pro = Product.GetByID(id).Single().MapToView();

            cartManager.AddToCart(user.Id, pro);
            return RedirectToAction("AddToCartPage","cart");
        }


        public IActionResult Delete(int id)
        {
            var cart = cartManager.GetAllCart().Where(c => c.ID == id).FirstOrDefault();

            cartManager.Delete(cart);
            return RedirectToAction("AddToCartPage");
        }
    }
}
