/****************************************************************
 * Provides extension method for the ModelBuilder Class         *
 * This method is used by AppDbContext.cs to seed the database  *
****************************************************************/

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstDemo.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee
                    {
                        Id = 1,
                        Name = "Kyle",
                        Department = Dept.IT,
                        Email = "kp@rm.com"
                    },

                    new Employee
                    {
                        Id = 2,
                        Name = "John",
                        Department = Dept.HR,
                        Email = "john@rm.com"
                    }
                );
        }
    }
}
