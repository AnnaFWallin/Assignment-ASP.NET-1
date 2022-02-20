using Assignment.WebAppMvc.Helpers;
using Assignment.WebAppMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.WebAppMvc.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            List<CartItemModel> shoppingCart = SessionHelper.GetObjectAsJson<List<CartItemModel>>(HttpContext.Session, "shoppingCart");

            if (shoppingCart == null)
                return RedirectToAction("NoItems", "ShoppingCart");

            ViewBag.ShoppingCart = SessionHelper.GetObjectAsJson<List<CartItemModel>>(HttpContext.Session, "shoppingCart");
            
            return View();
        }

        public IActionResult NoItems()
        {
            return View();
        }

        public int ItemExist(int id)
        {
            List<CartItemModel> shoppingCart = SessionHelper.GetObjectAsJson<List<CartItemModel>>(HttpContext.Session, "shoppingCart");
            for (int i = 0; i < shoppingCart.Count; i++)
            {
                if (shoppingCart[i].ProductId == id)
                    return i;
            }

            return -1;
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var product = new ProductModel();
            string url = "https://localhost:44382/api/products/" + id + "?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            using (var client = new HttpClient())
            {
                product = await client.GetFromJsonAsync<ProductModel>(url);
            }


            if (SessionHelper.GetObjectAsJson<List<CartItemModel>>(HttpContext.Session, "shoppingCart") == null)
            {
                List<CartItemModel> shoppingCart = new List<CartItemModel>();

                var cartItem = new CartItemModel(product.Id, product.Name, 1, product.Price);
                shoppingCart.Add(cartItem);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "shoppingCart", shoppingCart);
            }
            else
            {
                List<CartItemModel> shoppingCart = SessionHelper.GetObjectAsJson<List<CartItemModel>>(HttpContext.Session, "shoppingCart");
                int index = ItemExist(id);
                if (index != -1)
                    shoppingCart[index].Amount++;
                else
                    shoppingCart.Add(new CartItemModel(product.Id, product.Name, 1, product.Price));

                SessionHelper.SetObjectAsJson(HttpContext.Session, "shoppingCart", shoppingCart);
            }

            return NoContent();
        }

        public IActionResult RemoveFromCart(int id)
        {
            List<CartItemModel> shoppingCart = SessionHelper.GetObjectAsJson<List<CartItemModel>>(HttpContext.Session, "shoppingCart");
            var item = shoppingCart.FirstOrDefault(x => x.ProductId == id);

            if (item.Amount > 1)
                item.Amount--;
            else
                shoppingCart.Remove(item);

            SessionHelper.SetObjectAsJson(HttpContext.Session, "shoppingCart", shoppingCart);

            return RedirectToAction("Index", "ShoppingCart");
        }
                
    }
}
