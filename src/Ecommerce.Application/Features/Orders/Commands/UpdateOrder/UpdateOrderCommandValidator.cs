using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.identity;
using Ecommerce.Domain.order;
using Ecommerce.Domain.product;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateOrderCommandValidator(
            IBaseRepository<Product> productRepository,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;

            RuleFor(command => command)
                .MustAsync(IsOrderBelongToUser)
                .WithMessage("The specified order does not belong to the user.");

            RuleForEach(command => command.OrderResults)
                .MustAsync(IsValidProductAndPrice).WithMessage("Invalid product ID or price.");

            RuleFor(command => command.ApplicationUserId)
                .NotEmpty().WithMessage("User ID is required.")
                .MustAsync(DoesUserExist).WithMessage("User does not exist.");

            RuleFor(command => command.OrderTotalAmount)
                .GreaterThan(0).WithMessage("Order total amount must be greater than zero.");

            RuleFor(command => command)
                .Must(command => ValidateOrderTotalAmount(command))
                .WithMessage("Order total amount does not match the calculated amount.");

            RuleFor(command => command.OrderTotalDiscount)
                .GreaterThanOrEqualTo(0).WithMessage("Order total discount must be non-negative.");

            RuleFor(command => command.Street)
                .NotEmpty().WithMessage("Street address is required.")
                .MaximumLength(100).WithMessage("Street address cannot exceed 100 characters.");

            RuleFor(command => command.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City cannot exceed 50 characters.");

            RuleFor(command => command.PostalCode)
                .NotEmpty().WithMessage("Postal code is required.")
                .Matches(@"^\d{5}$").WithMessage("Postal code must be a 5-digit number.");

            RuleFor(command => command.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country cannot exceed 50 characters.");

            RuleForEach(command => command.OrderResults)
                .SetValidator(new OrderItemDtoValidator(_productRepository));
        }
        private async Task<bool> DoesUserExist(string userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null;
        }
        private async Task<bool> IsOrderBelongToUser(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Orders.FindByLamdaAsync(
                x => x.Id == command.OrderId && x.ApplicationUserId == command.ApplicationUserId,
                cancellationToken);
            return result != null;
        }

        // Ensure that the OrderTotalAmount matches the sum of the item quantities and prices
        private bool ValidateOrderTotalAmount(UpdateOrderCommand command)
        {
            var totalAmount = command.OrderResults
                .Sum(item => item.Quantity * item.Price);

            return totalAmount == command.OrderTotalAmount;
        }
        private async Task<bool> IsValidProductAndPrice(UpdateOrderItemsDto item, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetEntityByIdAsync(item.ProductId);
            if (product == null) return false;

            return item.Price == product.Price;
        }
    }

    // Validator for each OrderItem in UpdateOrderCommand
    public class OrderItemDtoValidator : AbstractValidator<UpdateOrderItemsDto>
    {
        private readonly IBaseRepository<Product> _productRepository;
        public OrderItemDtoValidator(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;

            RuleFor(item => item.ProductId)
                .NotEmpty().WithMessage("Product ID is required.")
                .MustAsync(DoesProductExist).WithMessage("Product does not exist.");

            RuleFor(item => item.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }

        private async Task<bool> DoesProductExist(Guid productId, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetEntityByIdAsync(productId);
            return product != null;
        }
    }
}