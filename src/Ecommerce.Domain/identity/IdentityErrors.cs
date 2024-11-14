using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.identity
{
    public class IdentityErrors
    {

        public static readonly Error NotApplicationUserWithThisId = Error.NotFound(
            code: "ApplicationUser.NotFound",
            description: "No Order with this Id");

        public static readonly Error NoApplicationUsersFound = Error.NotFound(
            code: "ApplicationUser.NotFound",
            description: "No Orders found.");

        public static readonly Error NoApplicationUsersWithThisEmailOrPasswordFound = Error.NotFound(
                code: "ApplicationUser.NotFound",
                description: "No Users With This Email Or Password found.");
        
    }
}
