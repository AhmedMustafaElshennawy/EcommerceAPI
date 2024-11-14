using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Products
{
    public record DeleteProductRequest(Guid productId);
    
}
