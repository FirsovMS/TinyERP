using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TinyErpWebService.Models.DTO;

namespace TinyErpWebService.Models
{
    public class EmployeeContext : IdentityDbContext<Employee>
    {
        public DbSet<Employee> Employers { get; set; }

        public EmployeeContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("orders");
            base.OnModelCreating(modelBuilder);
        }
    }
}
