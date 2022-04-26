using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MultiCoreApp.Core.Models;
using MultiCoreApp.DataAccessLayer.Configurations;
using MultiCoreApp.DataAccessLayer.Seeds;

namespace MultiCoreApp.DataAccessLayer
{
    public class MultiDbContext : DbContext
    {
        public MultiDbContext(DbContextOptions<MultiDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guid g1 = Guid.NewGuid();
            Guid g2 = Guid.NewGuid();
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            //modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);

            modelBuilder.ApplyConfiguration(new CategorySeed(new Guid[] {g1, g2}));
            modelBuilder.ApplyConfiguration(new ProductSeed(new Guid[] { g1, g2 }));

            

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

}
