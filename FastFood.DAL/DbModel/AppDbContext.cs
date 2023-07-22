using FastFood.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.DbModel
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Categories> categories { get; set; }
        public DbSet<Cities> cities { get; set; }
        public DbSet<Foods> foods { get; set; }
        public DbSet<Restaurants> restaurants { get; set; }
    }
}
