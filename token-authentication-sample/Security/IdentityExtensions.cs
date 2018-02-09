using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TokenAuthenticationSample.Security {
    public static class IdentityExtensions {
        public static string Identifier(this ClaimsPrincipal user) {
            string identifier = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(identifier)) {
                //This shouldn't happen if happen throw error to make it obvious!!!
                throw new InvalidOperationException("unable to get user identifier");
            }

            return identifier;
        }

        public static string Surname(this ClaimsPrincipal user) {
            return user.FindFirstValue(ClaimTypes.Surname);
        }
    }
}
