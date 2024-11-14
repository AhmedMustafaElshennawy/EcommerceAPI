using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.order;
using ErrorOr;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ErrorOr<Order>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork)=> _unitOfWork = unitOfWork;
        public async Task<ErrorOr<Order>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetOrderById(request.OrderId);
            return order;
        }
    }
}
