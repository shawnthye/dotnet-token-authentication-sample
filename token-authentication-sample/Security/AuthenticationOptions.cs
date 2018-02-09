using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace tokenauthenticationsample.Security
{
    public class CustomAuthOptions : AuthenticationSchemeOptions
    {
        
        public const string DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        public string Scheme => DefaultAuthenticateScheme;
        public StringValues AuthKey { get; set; }
    }


    public class CustomAuthHandler : AuthenticationHandler<CustomAuthOptions>
    {
        public CustomAuthHandler(IOptionsMonitor<CustomAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            string authorization = Request.Headers["Authorization"];

            string token = null;

            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authorization.Substring("Bearer ".Length).Trim();
            }

            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(AuthenticateResult.NoResult()) ;
            }

            var identities = new List<ClaimsIdentity> { new ClaimsIdentity("custom auth type") };
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), Options.Scheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
           

            return Task.FromResult(AuthenticateResult.Success());
        }
    }





    public class TokenRequirement : IAuthorizationRequirement
    {
        
    }

    public class MyAuthenticationHandler : Microsoft.AspNetCore.Authorization.AuthorizationHandler<TokenRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TokenRequirement requirement)
        {
            
            throw new NotImplementedException();
        }
    }
}
