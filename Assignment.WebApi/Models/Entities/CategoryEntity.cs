using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.WebApi.Models.Entities
{
    public class CategoryEntity
    {
        public CategoryEntity(string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}