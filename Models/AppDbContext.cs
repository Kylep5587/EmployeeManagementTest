/* ******************************************
 * Used to query and save instances of      *
 *  Employees class.                        *
 * Translates Linq queries into SQL queries *
********************************************/

using FirstDemo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstDemo.Models
{
    public class AppDbContext : IdentityDbContext //? Allows for use of core identity; IdentityDbContext class inherits from DbContext class so we don't need to do it explicitly
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) // Calls the constructor of the DbContext (base) class
        {

        }

        // DbSet property must be set for each type in the project
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Calls the base class OnModelCreating method and passes it the incoming model builder object
            modelBuilder.Seed(); // Extension method from ModelBuilderExtension.cs for seeding database
        }
    }
}
