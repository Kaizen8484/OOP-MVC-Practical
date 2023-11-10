using OOP_MVC_Practical.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace OOP_MVC_Practical.Data
{
    public class mvcDbContext : DbContext
    {
        public mvcDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
