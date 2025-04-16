using Ikea.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.DAL.Persistence.Data.Configurations.Departments
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Entities.Departments.Department>
    {
        public void Configure(EntityTypeBuilder<Entities.Departments.Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("varchar(30)");
            builder.Property(D => D.Description).HasColumnType("varchar(50)");
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETDATE()");
            //builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");

            builder.HasMany(D => D.Employees)
                   .WithOne(E => E.Department)
                   .HasForeignKey(E => E.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);
        }   
    }
}
