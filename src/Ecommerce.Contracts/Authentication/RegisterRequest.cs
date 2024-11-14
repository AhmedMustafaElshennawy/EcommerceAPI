using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Authentication
{
    public record RegisterRequest(
        string firstName,
        string lastName,
        string userName,
        string email,
        string password,
        string passwordComfirmation, 
        string phoneNumber);
}
