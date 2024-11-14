using Ecommerce.Contracts.Card_CardItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Authentication
{
    public record shoppingCardResponse(
        Guid ShoppingCartId,
        string ApplicationUserId,
        DateTime CreatedDate);
}
