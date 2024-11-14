using Ecommerce.Application.Features.Orders.Commands.CreateOrder;
using Ecommerce.Application.Features.Orders.Commands.UpdateOrder;
using Ecommerce.Application.Features.review.Commands.CreateReview;
using Ecommerce.Application.Features.review.Commands.UpdateReview;
using Ecommerce.Application.Features.review.Queries.GetReviewById;
using Ecommerce.Contracts.Order;
using Ecommerce.Contracts.Reviews;
using Ecommerce.Domain.order;
using Ecommerce.Domain.orderItem;
using Ecommerce.Domain.review;
using Mapster;

public static class MappingConfig
{
    public static void ConfigureMappings()
    {
        TypeAdapterConfig<CreateOrderRequest, CreateOrderCommand>.NewConfig()
            .Map(dest => dest.OrderItems, src => src.OrderItems.Select(item => item.Adapt<OrderItemDto>()).ToList());

        TypeAdapterConfig<OrderItemRequest, OrderItemDto>.NewConfig();

        TypeAdapterConfig<UpdateOrderRequest, UpdateOrderCommand>.NewConfig()
            .Map(dest => dest.OrderResults, src => src.OrderResults.Select(item => item.Adapt<UpdateOrderItemsDto>()).ToList());

        TypeAdapterConfig<UpdateOrderItemsRequest, UpdateOrderItemsDto>.NewConfig();

        TypeAdapterConfig<UpdateOrderCommand, UpdateOrderResponse>.NewConfig()
            .Map(dest => dest.Id, src => src.OrderId)
            .Map(dest => dest.OrderItems, src => src.OrderResults.Select(item => item.Adapt<UpdateOrderItemsResponse>()).ToList());

        TypeAdapterConfig<UpdateOrderItemsDto, UpdateOrderItemsResponse>.NewConfig();


        TypeAdapterConfig<Order, GetOrderByIdResponse>.NewConfig()
            .Map(dest => dest.OrderItems, src => src.OrderItems.Adapt<List<OrderItemResponse>>())
            .Map(dest => dest.ApplicationUserId, src => src.ApplicationUserId);

        TypeAdapterConfig<OrderItem, OrderItemResponse>.NewConfig()
            .Map(dest => dest.ProductId, src => src.ProductId)
            .Map(dest => dest.Quantity, src => src.Quantity)
            .Map(dest => dest.Price, src => src.Price);

        TypeAdapterConfig<Review, CreateReviewResponse>.NewConfig();
        TypeAdapterConfig<CreareReviewRequest, CreateReviewCommand>.NewConfig();

        TypeAdapterConfig<GetReviewByIdRequest, DeleteReviewCommand>.NewConfig();
        TypeAdapterConfig<Review, GetReviewByIdResponse>.NewConfig();

        TypeAdapterConfig<UpdateReviewRequest, UpdateReviewCommand>.NewConfig();
        TypeAdapterConfig<Review, UpdateReviewResponse>.NewConfig();
            
    }
}