using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiCoreApp.Core.Models;

namespace MultiCoreApp.Core.IntRepository
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<Product> GetWithCategoryByIdAsync(Guid proId);
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();
    }
}
