using Ikea.DAL.Entities.Departments;
using Ikea.DAL.Entities.Employees;
using Ikea.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ikea.DAL.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure Connection String
            // optionsBuilder.UseSqlServer("Server=. ; DataBase = Ikea02 ; Trusted_Connection = true; TrustServerCertificate = true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
        
        #region Dbsets
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; } 

        #endregion
   
    
    }
}
