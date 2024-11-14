using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.order;
using Ecommerce.Domain.orderItem;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ErrorOr<Order>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
      
        public async Task<ErrorOr<Order>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetOrderById(request.OrderId);

            order.OrderTotalAmount = request.OrderTotalAmount;
            order.OrderTotalDiscount = request.OrderTotalDiscount;
            order.Street = request.Street;
            order.City = request.City;
            order.PostalCode = request.PostalCode;
            order.Country = request.Country;
            order.OrderItems.Clear();
            foreach (var itemDto in request.OrderResults)
            {
                var orderItem = new OrderItem
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    Price = itemDto.Price
                };

                order.OrderItems.Add(orderItem);
            }

            await _unitOfWork.Orders.UpdateEntityAsync(order);
            await _unitOfWork.CompleteAsync();

            return order;
        }
    }

}