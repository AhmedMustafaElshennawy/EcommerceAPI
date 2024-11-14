using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Features.Orders.Commands.UpdateOrder;
using Ecommerce.Domain.order;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetOrderByIdQueryValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(Query => Query.OrderId)
                .NotEmpty().WithMessage("Order ID is required.")
                .MustAsync(DoesOrderExist).WithMessage("No order does not exist with GUID.")
                .NotEqual(Guid.Empty).WithMessage("Order ID cannot be an empty GUID.");
        }
        private async Task<bool> DoesOrderExist(Guid Id, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetOrderById(Id);
            return order != null;
        }  
    }
}