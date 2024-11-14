using Ecommerce.Domain.identity;
using Ecommerce.Domain.orderItem;
using Ecommerce.Domain.product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.order
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal OrderTotalAmount { get; set; }
        public decimal OrderTotalDiscount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string? Country { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}
