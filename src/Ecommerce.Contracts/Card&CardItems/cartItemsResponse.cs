using Ecommerce.Contracts.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Card_CardItems
{
    public record cartItemsResponse
    {
        public IEnumerable<Guid> CartItemIds { get; }
        public IEnumerable<Guid> ShoppingCartIds { get; }
        public IEnumerable<Guid> ProductIds { get; }
        public IEnumerable<int> Quantities { get; }

        public cartItemsResponse(
            IEnumerable<Guid> cartItemIds,
            IEnumerable<Guid> shoppingCartIds,
            IEnumerable<Guid> productIds,
            IEnumerable<int> quantities)
        {
            CartItemIds = cartItemIds;
            ShoppingCartIds = shoppingCartIds;
            ProductIds = productIds;
            Quantities = quantities;
        }


        //var carditem = new cartItemsResponse(
        //     result.Value.shoppingCart.CartItems.Select(X => X.CartItemId),
        //     result.Value.shoppingCart.CartItems.Select(X => X.ShoppingCartId), ==> //var cartItemResponse = result.Value.shoppingCart.Adapt<cartItemsResponse>();
        //     result.Value.shoppingCart.CartItems.Select(X => X.ProductId),
        //     result.Value.shoppingCart.CartItems.Select(X => X.Quantity)
        //    );

    }
}
