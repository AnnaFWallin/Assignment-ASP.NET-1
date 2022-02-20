namespace Assignment.WebApi.Models
{
    public class ProductModel
    {
        public ProductModel()
        {

        }

        public ProductModel(string name, string urlLink, string description, decimal price, int categoryId)
        {
            Name = name;
            UrlLink = urlLink;
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }

        public ProductModel(int id, string name, string urlLink, string description, decimal price, int categoryId)
        {
            Id = id;
            Name = name;
            UrlLink = urlLink;
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlLink { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
