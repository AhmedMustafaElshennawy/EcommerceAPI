using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Features.Orders.Commands.UpdateOrder;
using Ecommerce.Domain.identity;
using Ecommerce.Domain.product;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateOrderCommandValidator(
            IBaseRepository<Product> productRepository,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _productRepository = productRepository;

            RuleForEach(command => command.OrderItems)
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

            RuleForEach(command => command.OrderItems)
                .SetValidator(new OrderItemDtoValidator(_productRepository));
        }

        private async Task<bool> DoesUserExist(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user != null;
        }
        // Ensure that the OrderTotalAmount matches the sum of the item quantities and prices
        private bool ValidateOrderTotalAmount(CreateOrderCommand command)
        {
            var totalAmount = command.OrderItems
                .Sum(item => item.Quantity * item.Price);

            return totalAmount == command.OrderTotalAmount;
        }

        private async Task<bool> IsValidProductAndPrice(OrderItemDto item, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetEntityByIdAsync(item.ProductId);
            if (product == null) return false;

            // Check if the provided price matches the product's actual price
            return item.Price == product.Price;
        }
    }

    // Validator for each OrderItem in CreateOrderRequest
    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
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