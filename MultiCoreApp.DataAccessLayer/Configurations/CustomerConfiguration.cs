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
    public class CustomerConfiguration:IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(50);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(150);
            builder.Property(n => n.Address).HasMaxLength(250);
            builder.Property(n => n.City).IsRequired().HasMaxLength(50);
            builder.Property(n => n.Phone).IsRequired().HasMaxLength(50);
            builder.ToTable("tblCustomer");
            
        }

    }
}
