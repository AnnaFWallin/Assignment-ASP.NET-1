using Assignment.WebAppMvc.Models;
using Assignment.WebAppMvc.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Assignment.WebAppMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            IEnumerable<ProductModel> products = new List<ProductModel>();
            using (var client = new HttpClient())
            {
                products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:44382/api/products?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
            }

            return View(products);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}