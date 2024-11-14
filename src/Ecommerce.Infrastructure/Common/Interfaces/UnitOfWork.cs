using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.catgory;
using Ecommerce.Domain.order;
using Ecommerce.Domain.orderItem;
using Ecommerce.Domain.product;
using Ecommerce.Domain.review;
using Ecommerce.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Common.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IOrderRepository Orders { get; set; }
        public IBaseRepository<Product> Products { get; set; }
        public IBaseRepository<OrderItem> OrderItems { get; set; }
        public IReviewRepository Reviews { get; set; }
        public IBaseRepository<Category> Categories { get; set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            Orders = new OrderRepository(context);
            Products = new BaseRepository<Product>(context);
            OrderItems = new BaseRepository<OrderItem>(context);
            Reviews = new ReviewRepository(context);
            Categories = new BaseRepository<Category>(context);
            _context = context; 
        }
        public int Complete() => _context.SaveChanges();
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}