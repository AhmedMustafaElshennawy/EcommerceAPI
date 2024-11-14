using Ecommerce.Domain.catgory;
using Ecommerce.Domain.order;
using Ecommerce.Domain.orderItem;
using Ecommerce.Domain.product;
using Ecommerce.Domain.review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IOrderRepository Orders {  get; }
        public IBaseRepository<Product> Products {  get; }
        public IBaseRepository<OrderItem> OrderItems {  get; }
        public IReviewRepository Reviews {  get; }
        public IBaseRepository<Category> Categories {  get; }
        Task<int> CompleteAsync();
        int Complete();
    }
}
