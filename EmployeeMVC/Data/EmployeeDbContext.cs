using EmployeeMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Data
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<EmployeeViewModel> Employees { get; set; }
        protected EmployeeDbContext()
        {
        }

        public EmployeeDbContext(DbContextOptions options): base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-QM194TV4\SQLEXPRESS;Database=EmployeeCrudDB;Trusted_Connection=True");


        }
    }
}
