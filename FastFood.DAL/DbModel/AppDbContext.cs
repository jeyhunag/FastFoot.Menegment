using FastFood.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.DbModel
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Categories> categories { get; set; }
        public DbSet<Cities> cities { get; set; }
        public DbSet<Foods> foods { get; set; }
        public DbSet<Restaurants> restaurants { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<Courier> couriers { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<SiteInfo> SiteInfos { get; set; }
        public DbSet<MetaDescription> MetaDescriptions { get; set; }
        public DbSet<MetaKeyword> MetaKeywords { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<BannersImage> BannersImages { get; set; }
        public DbSet<RestaurantsImage> RestaurantsImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Foods>().HasOne(x => x.Categories)
           .WithMany(x => x.Foods).HasForeignKey(x => x.CategoriesId);

            modelBuilder.Entity<Orders>()
            .HasIndex(ub => new { ub.CourierId, ub.CitiesId, ub.FoodsId, ub.RestaurantsId })
            .IsUnique();

            modelBuilder.Entity<Orders>()
                .HasOne(ub => ub.Courier)
                .WithMany(au => au.Orders)
                .HasForeignKey(ub => ub.CourierId)
                .OnDelete(DeleteBehavior.NoAction); // Change this



            modelBuilder.Entity<Orders>()
               .HasOne(ub => ub.Cities)
               .WithMany(au => au.Orders)
               .HasForeignKey(ub => ub.CitiesId)
               .OnDelete(DeleteBehavior.NoAction); // Change this

            modelBuilder.Entity<Orders>()
               .HasOne(ub => ub.Foods)
               .WithMany(au => au.Orders)
               .HasForeignKey(ub => ub.FoodsId)
               .OnDelete(DeleteBehavior.NoAction); // Change this

            modelBuilder.Entity<Orders>()
               .HasOne(ub => ub.Restaurants)
               .WithMany(au => au.Orders)
               .HasForeignKey(ub => ub.RestaurantsId)
               .OnDelete(DeleteBehavior.NoAction); // Change this


        }
    }
}
