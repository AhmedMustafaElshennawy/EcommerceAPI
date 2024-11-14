using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.order;
using Ecommerce.Domain.orderItem;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Commands.CreateOrder
{

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateOrderCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<ErrorOr<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                ApplicationUserId = request.ApplicationUserId.ToString(),
                OrderTotalAmount = request.OrderTotalAmount,
                OrderTotalDiscount = request.OrderTotalDiscount,
                Street = request.Street,
                City = request.City,
                PostalCode = request.PostalCode,
                Country = request.Country,
                OrderDate = DateTime.UtcNow,
            };

            var orderItems = request.OrderItems.Select(item => new OrderItem
            {
                OrderId = orderId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList();
            await _unitOfWork.Orders.CreateEntityAsync(order);
            await _unitOfWork.OrderItems.CreateEntitiesAsync(orderItems);

            await _unitOfWork.CompleteAsync();
            return order.Id; 
        }
    }
}