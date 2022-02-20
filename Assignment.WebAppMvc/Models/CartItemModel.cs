using System.ComponentModel.DataAnnotations;

namespace Assignment.WebAppMvc.Models
{
    public class CartItemModel
    {
       
        public CartItemModel(int productId, string productName, int amount, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            Amount = amount;
            Price = price;
        }

        public int ProductId { get; set; }
        [Display(Name = "Produkt")]
        public string ProductName { get; set; }
        [Display(Name = "Antal")]
        public int Amount { get; set; }
        [Display(Name = "Pris")]
        public decimal Price { get; set; }
    }
}
