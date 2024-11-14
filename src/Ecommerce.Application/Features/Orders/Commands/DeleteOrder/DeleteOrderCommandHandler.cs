using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.order;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Order> _orderRepository;

        public DeleteOrderCommandHandler(
            IUnitOfWork unitOfWork, 
            IBaseRepository<Order> orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetEntityByIdAsync(request.orderId);
            if (order is null)
                return Error.NotFound(description: "There is no order with this Id , Please enter a valid Id");

            await _orderRepository.DeleteEntityAsync(order);
            var result = await _unitOfWork.CompleteAsync();
            return Unit.Value;
        }
    }
}
