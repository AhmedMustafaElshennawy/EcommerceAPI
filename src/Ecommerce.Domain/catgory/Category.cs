using Ecommerce.Domain.product;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.catgory
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CrearedOn { get; set; }
        public ICollection<Product> Products { get; set; }= new HashSet<Product>();
    }
}
