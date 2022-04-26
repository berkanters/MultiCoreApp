using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using MultiCoreApp.Core.Models;

namespace MultiCoreApp.DataAccessLayer.Seeds
{
    public class ProductSeed:IEntityTypeConfiguration<Product>
    {
        private readonly Guid[] _guids;

        public ProductSeed(Guid[] guids)
        {
            _guids=guids;
        }
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            
            builder.HasData(
                new Product
                {
                    Id = Guid.NewGuid(), Stock = 100, Name = "Dolma Kalem", Price = 12.53m, CategoryId = _guids[0]
                },
                new Product
                {
                    Id = Guid.NewGuid(), Stock = 12, Name = "Tükenmez Kalem", Price = 122.13m, CategoryId = _guids[0]
                },
                new Product
                {
                    Id = Guid.NewGuid(), Stock = 1000, Name = "Kurşun Kalem", Price = 1.49m, CategoryId = _guids[0]
                },
                new Product
                {
                    Id = Guid.NewGuid(), Stock = 100, Name = "Çizgili Defter", Price = 12.32m, CategoryId = _guids[1]
                },
                new Product
                {
                    Id = Guid.NewGuid(), Stock = 1000, Name = "Kareli Defter", Price = 15.21m, CategoryId = _guids[1]
                },
                new Product
                {
                    Id = Guid.NewGuid(), Stock = 1000, Name = "Dumdüz Defter", Price = 10.79m, CategoryId = _guids[1]
                }
            );
        }
    }
}
