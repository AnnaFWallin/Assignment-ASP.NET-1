using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.WebApi.Models.Entities
{
    public class OrderEntity
    {
        public OrderEntity()
        {

        }
        public OrderEntity(string customerId, string customerName, DateTime orderDate, decimal totalprice)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            OrderDate = orderDate;
            Totalprice = totalprice;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime OrderDate { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Totalprice { get; set; }

        public ICollection<OrderRowEntity> OrderRows { get; set; }

    }

}
