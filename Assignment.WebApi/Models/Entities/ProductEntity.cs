using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.WebApi.Models.Entities
{
    public class ProductEntity
    {
        public ProductEntity(string name, string description, decimal price, int categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }

        public ProductEntity(string name, string urlLink, string description, decimal price, int categoryId)
        {
            Name = name;
            UrlLink = urlLink;
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        public string UrlLink { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int CategoryId { get; set; }

        
        public CategoryEntity Category { get; set; }
        public virtual ICollection<OrderRowEntity> OrderRows { get; }

    }   
}
