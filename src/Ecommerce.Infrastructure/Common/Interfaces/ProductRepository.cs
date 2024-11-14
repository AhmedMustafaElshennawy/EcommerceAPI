using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.identity;
using Ecommerce.Domain.order;
using Ecommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Common.Interfaces
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            var order =  await _context.Orders
                             .Include(o => o.OrderItems)
                             .FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }
    }
}
