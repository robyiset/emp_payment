using app.Models.database.Tables;
using app.Models.database.Views;
using Microsoft.EntityFrameworkCore;

namespace app.Models.database
{
    public class EmpPaymentContext : DbContext
    {
        public EmpPaymentContext() { }
        public EmpPaymentContext(DbContextOptions options) : base(options) { }

        public DbSet<employees> employees { get; set; }
        public DbSet<payrolls> payrolls { get; set; }
        public DbSet<vw_employee_payroll> vw_employee_payroll { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vw_employee_payroll>(entity =>
                        {
                            entity.HasNoKey();
                        });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {

        }
    }
}