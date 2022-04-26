using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiCoreApp.Core.Models;

namespace MultiCoreApp.DataAccessLayer.Configurations
{
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(s => s.Stock).IsRequired();
            builder.Property(s => s.CategoryId).IsRequired();
            builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
            builder.ToTable("tblProducts");
        }
    }
}
