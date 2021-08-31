using ITB1704Application.Model;
using Microsoft.EntityFrameworkCore;

namespace ITB1704Application
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Test> Tests { get; set; }

    }
}