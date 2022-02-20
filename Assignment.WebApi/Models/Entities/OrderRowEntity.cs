using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.WebApi.Models.Entities
{
    public class OrderRowEntity
    {
        public OrderRowEntity()
        {

        }
        public OrderRowEntity(int orderId, int productId, int amount, string productName, decimal price)
        {
            OrderId = orderId;
            ProductId = productId;
            Amount = amount;
            ProductName = productName;
            Price = price;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int ProductId { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public OrderEntity Order { get; set; }
        public ProductEntity Product { get; set; }
    }

}