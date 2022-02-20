using Assignment.WebApi.Models.Entities;

namespace Assignment.WebApi.Models
{
    public class OrderModel
    {
        public OrderModel()
        {

        }
        public OrderModel(string customerId, string customerName, DateTime orderDate, decimal totalPrice)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
        }

        public OrderModel(int id, string customerId, string customerName, DateTime orderDate, decimal totalPrice)
        {
            Id = id;
            CustomerId = customerId;
            CustomerName = customerName;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
        }

        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
