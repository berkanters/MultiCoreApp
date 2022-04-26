using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiCoreApp.Core.IntRepository;

namespace MultiCoreApp.Core.IntUnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        ICustomerRepository Customer { get; }
        Task CommitAsync();
        void Commit();
    }
}
