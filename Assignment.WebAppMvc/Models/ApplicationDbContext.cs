using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Assignment.WebAppMvc.Models;

namespace Assignment.WebAppMvc.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Assignment.WebAppMvc.Models.ProductModel> ProductModel { get; set; }
    }
}
