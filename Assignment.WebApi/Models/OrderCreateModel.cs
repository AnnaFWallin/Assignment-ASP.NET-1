using Assignment.WebApi.Models.Entities;

namespace Assignment.WebApi.Models
{
    public class OrderCreateModel
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
