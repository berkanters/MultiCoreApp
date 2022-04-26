using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCoreApp.Core.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; } = 0;
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid CategoryId { get; set; }
        //[ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
