using Ecommerce.Domain.order;
using Ecommerce.Domain.review;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Order> orders { get; set; } = new HashSet<Order>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
