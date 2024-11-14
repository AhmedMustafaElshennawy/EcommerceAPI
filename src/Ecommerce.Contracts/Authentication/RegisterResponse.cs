using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Authentication
{
    public record RegisterResponse(
        string id,
        string firstName,
        string lastName,
        string userName,
        string email,
        string phoneNumber,
        string token
        );
}
