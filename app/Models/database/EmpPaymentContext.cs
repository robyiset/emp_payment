using app.Models.database.Tables;
using Microsoft.EntityFrameworkCore;

namespace app.Models.database
{
    public class EmpPaymentContext : DbContext
    {
        public EmpPaymentContext() { }
        public EmpPaymentContext(DbContextOptions options) : base(options) { }

        public DbSet<employees> employees { get; set; }
        public DbSet<payrolls> payrolls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {

        }
    }
}