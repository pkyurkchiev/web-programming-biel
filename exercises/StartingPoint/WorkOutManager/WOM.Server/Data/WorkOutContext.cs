using Microsoft.EntityFrameworkCore;
using WOM.Models;

namespace WOM.Server.Data
{
    public class WorkOutContext : DbContext
    {
        public WorkOutContext(DbContextOptions options) : base(options)
        { }

        public DbSet<WorkOut> WorkOutList { get; set; }
    }

}
