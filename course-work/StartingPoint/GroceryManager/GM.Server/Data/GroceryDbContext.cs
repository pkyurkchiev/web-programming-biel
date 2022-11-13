using Microsoft.EntityFrameworkCore;
using GM.Models;

namespace GM.Server.Data
{
    public class GroceryDbContext : DbContext
    {
        public GroceryDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Grocery> Groceries { get; set; }
    }

}
