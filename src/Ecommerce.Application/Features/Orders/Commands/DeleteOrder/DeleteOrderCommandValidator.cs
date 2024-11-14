using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.order;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        private bool BeAValidGuid(Guid productId) => productId != Guid.Empty;
        private readonly IBaseRepository<Order> _productRepository;
        public DeleteOrderCommandValidator(IBaseRepository<Order> productRepository)

        {
            _productRepository = productRepository;

            RuleFor(command => command.orderId)
                .NotEmpty().WithMessage("Order ID is required.")
                .Must(BeAValidGuid).WithMessage("Order ID must be a valid GUID.")
                .MustAsync(DoesOrderExists).WithMessage("Order does not exist.");
        }
        public async Task<bool> DoesOrderExists(Guid id, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetEntityByIdAsync(id);
            return result != null;
        }
    }
}
