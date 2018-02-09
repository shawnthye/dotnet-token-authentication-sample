using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace TokenAuthenticationSample.Security {
    public class BearerTokenIdentity : IIdentity {
        public string AuthenticationType => BearerTokenOptions.DefaultAuthenticateScheme;

        public bool IsAuthenticated => true;

        public string Name => "User";
    }
}
