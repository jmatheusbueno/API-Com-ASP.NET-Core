using Microsoft.EntityFrameworkCore;
using WEB.API.Models;

namespace WEB.API.DataAnnotations
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
    }
}