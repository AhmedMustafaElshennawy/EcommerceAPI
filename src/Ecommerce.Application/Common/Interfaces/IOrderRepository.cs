using Ecommerce.Domain.identity;
using Ecommerce.Domain.order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Common.Interfaces
{
    public interface IOrderRepository: IBaseRepository<Order>
    {
        Task<Order> GetOrderById(Guid id);
    }
}
