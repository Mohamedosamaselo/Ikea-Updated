using Ikea.DAL.Common.Enums;
using Ikea.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.DAL.Persistence.Data.Configurations.Employees
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Entities.Employees.Employee>
    {
        void IEntityTypeConfiguration<Entities.Employees.Employee>.Configure(EntityTypeBuilder<Entities.Employees.Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();

            builder.Property(E => E.Address).HasColumnType("varchar(100)");

            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");

            builder.Property(E => E.Gender)
                   .HasConversion(

                (Gender) => Gender.ToString(),// save in DB As String

                (Gender) => (Gender)Enum.Parse(typeof(Gender), Gender) // When Returned it will Return as int  
                                  );

            builder.Property(E => E.EmployeeType)
                .HasConversion(

                (EmployeeType) => EmployeeType.ToString(),// save in DB As String
      
                (EmployeeType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), EmployeeType) // When Returned it will Return as int  

                                 );

            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETUTCDATE()");


        }
    }
}
