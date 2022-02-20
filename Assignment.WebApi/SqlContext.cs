using Assignment.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.WebApi
{
    public class SqlContext : DbContext

    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        protected SqlContext()
        {
        }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<OrderEntity> Orders { get; set; }
        public virtual DbSet<OrderRowEntity> OrderRows { get; set; }


    }
}
