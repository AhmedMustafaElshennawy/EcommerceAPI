using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.catgory;
using Ecommerce.Domain.orderItem;
using Ecommerce.Domain.review;

namespace Ecommerce.Domain.product
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; } = 0;
        public DateTime CreatedOn { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
