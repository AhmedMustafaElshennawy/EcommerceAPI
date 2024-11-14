using Ecommerce.Domain.identity;
using Ecommerce.Domain.product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.review
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid ProductId { get; set; }
        public string ApplicationUserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        // Navigation properties
        public Product Product { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
