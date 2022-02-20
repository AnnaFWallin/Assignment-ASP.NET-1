namespace Assignment.WebAppMvc.Models
{
    public class OrderRowModel
    {
        public OrderRowModel()
        {

        }
        public OrderRowModel(int productId, int amount, string productName, decimal price)
        {
            ProductId = productId;
            Amount = amount;
            ProductName = productName;
            Price = price;
        }

        public OrderRowModel(int orderId, int productId, int amount, string productName, decimal price)
        {
            OrderId = orderId;
            ProductId = productId;
            Amount = amount;
            ProductName = productName;
            Price = price;
        }

        public OrderRowModel(int id, int orderId, int productId, int amount, string productName, decimal price)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Amount = amount;
            ProductName = productName;
            Price = price;
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
