using Models;
using ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maneger
{
    public class CartManager : MainManeger<CartItem>
    {
        public CartManager(MyDBContext context) : base(context)
        {
        }

        public void AddToCart(string userID, ProductViewModel productView)
        {
            var item = base.GetAll().FirstOrDefault(c=>c.ProductID == productView.ID);
            if (item != null)
            {
                item.SupPrice += productView.Price;
                item.Quantity += productView.Quantity;
                base.Update(item);
            }
            else
            {
                item = new CartItem
                {
                    ProductID = (int)productView.ID,
                    UserID = userID,
                    Quantity = productView.Quantity,
                    SupPrice = productView.Price
                };
            }

            base.Add(item);
        }

        public List<CartItem> GetAllCart()
        {
            List<CartItem> cartItems = new List<CartItem>();

            foreach (var item in base.GetAll().ToList())
            {
                if (item.ProductID == item.ProductID+1)
                {
                    cartItems.Add(item);
                }
            }

            return base.GetAll().ToList();
        }

        public void Delete(CartItem cartItem) {
            base.Delete(cartItem);
        }
    }
}
