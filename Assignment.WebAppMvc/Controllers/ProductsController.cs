using Assignment.WebAppMvc.Helpers;
using Assignment.WebAppMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.WebAppMvc.Controllers
{
    public class ProductsController : Controller
    {

        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductModel> products = new List<ProductModel>();
            using (var client = new HttpClient())
            {
                products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:44382/api/products?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
            }

            return View(products);
        }
        public async Task<IActionResult> Books()
        {

            IEnumerable<ProductModel> books = new List<ProductModel>();
            using (var client = new HttpClient())
            {
                books = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:44382/api/products/1/category?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
            }

            return View(books);
        }
        public async Task<IActionResult> Bookmarks()
        {
            IEnumerable<ProductModel> bookmarks = new List<ProductModel>();
            using (var client = new HttpClient())
            {
                bookmarks = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:44382/api/products/2/category?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
            }

            return View(bookmarks);
        }
        public async Task<IActionResult> Tea()
        {
            IEnumerable<ProductModel> tea = new List<ProductModel>();
            using (var client = new HttpClient())
            {
                tea = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:44382/api/products/3/category?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
            }

            return View(tea);
        }
        public async Task<IActionResult> Teacups()
        {
            IEnumerable<ProductModel> teacups = new List<ProductModel>();
            using (var client = new HttpClient())
            {
                teacups = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:44382/api/products/4/category?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
            }

            return View(teacups);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = new ProductModel();
            string url = "https://localhost:44382/api/products/" + id + "?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            using (var client = new HttpClient())
            {
                product = await client.GetFromJsonAsync<ProductModel>(url);
            }

            return View(product);
        }

    }
}
