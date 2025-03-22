using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HomeManagement.Model
{
    public class HomeManagementDbContext: DbContext
    {
        public HomeManagementDbContext(DbContextOptions<HomeManagementDbContext> options): base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}