using Assignment.WebAppMvc.Helpers;
using Assignment.WebAppMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.WebAppMvc.Controllers
{
    public class OrderController : Controller
    {
            
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public OrderController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            IEnumerable<OrderModel> orders = new List<OrderModel>();

            var url = "https://localhost:44382/api/Order/" + user.Id + "/CustomerID?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            using (var client = new HttpClient())
            {
                orders = await client.GetFromJsonAsync<IEnumerable<OrderModel>>(url);
            }
                    
            return View(orders);
        }
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var order = new OrderModel();
            string url = "https://localhost:44382/api/order/" + id + "/Id?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            using (var client = new HttpClient())
            {
                order = await client.GetFromJsonAsync<OrderModel>(url);
            }

            IEnumerable<OrderRowModel> orderRows = new List<OrderRowModel>();
            var urlRows = "https://localhost:44382/api/OrderRow/" + order.Id + "/OrderId?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            using (var client = new HttpClient())
            {
                orderRows = await client.GetFromJsonAsync<IEnumerable<OrderRowModel>>(urlRows);
            }

            return View(orderRows);
        }

        public async Task<ActionResult<OrderModel>> CreateOrder()
        {
            List<CartItemModel> shoppingCart = SessionHelper.GetObjectAsJson<List<CartItemModel>>(HttpContext.Session, "shoppingCart");

            if (shoppingCart.Count == 0)
                return RedirectToAction("NoItems", "ShoppingCart");

            else
            {
                if (_signInManager.IsSignedIn(User))
                {
                    var user = await _userManager.GetUserAsync(User);
                    var userName = user.FirstName + " " + user.LastName;
                    decimal totalPrice = 0;

                    foreach(CartItemModel model in SessionHelper.GetObjectAsJson<List<CartItemModel>>(HttpContext.Session, "shoppingCart"))
                        totalPrice += model.Price*model.Amount;

                    var orderModel = new OrderModel(user.Id, userName, DateTime.Now, totalPrice);                

                    using (var client = new HttpClient())
                    {
                        await client.PostAsJsonAsync<OrderModel>("https://localhost:44382/api/Order?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", orderModel);
                    }

                    var orderRows = new List<OrderRowModel>();
                    foreach (CartItemModel model in SessionHelper.GetObjectAsJson<List<CartItemModel>>(HttpContext.Session, "shoppingCart"))
                        orderRows.Add(new OrderRowModel(model.ProductId, model.Amount, model.ProductName, model.Price));

                    foreach(var model in orderRows)
                    {
                        using (var client = new HttpClient())
                        {
                            await client.PostAsJsonAsync<OrderRowModel>("https://localhost:44382/api/OrderRow?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", model);
                        }
                    }

                    shoppingCart.Clear();

                    SessionHelper.SetObjectAsJson(HttpContext.Session, "shoppingCart", shoppingCart);
                    

                    return View();
                }

                else
                    return RedirectToAction("Unauthorized");
            }

        }

        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}
