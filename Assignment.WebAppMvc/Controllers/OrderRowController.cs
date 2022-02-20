using Assignment.WebAppMvc.Helpers;
using Assignment.WebAppMvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.WebAppMvc.Controllers
{
    public class OrderRowController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public OrderRowController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult<OrderRowModel>> CreateOrderRow(OrderRowModel model)
        {
            if (SessionHelper.GetObjectAsJson<List<CartItemModel>>(HttpContext.Session, "shoppingCart") == null)
                return BadRequest();


            else
            {
                if (_signInManager.IsSignedIn(User))
                {

                    var orderRowModel = new OrderRowModel(model.OrderId, model.ProductId, model.Amount, model.ProductName, model.Price);
                    

                    using (var client = new HttpClient())
                    {
                        await client.PostAsJsonAsync<OrderRowModel>("https://localhost:44382/api/OrderRow?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", orderRowModel);
                    }

                    return View();
                }

                else
                    return BadRequest("Du måste logga in för att göra en beställning");
            }
        }
    }
}
