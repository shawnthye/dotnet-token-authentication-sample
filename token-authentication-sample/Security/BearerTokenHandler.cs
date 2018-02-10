using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TokenAuthenticationSample.Models;

namespace TokenAuthenticationSample.Security {
    public class BearerTokenHandler : AuthenticationHandler<BearerTokenOptions> {
        public BearerTokenHandler(IOptionsMonitor<BearerTokenOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock) {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {

            string authorization = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorization)) {
                var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Error() { errorDescription = "Missing Authorization" }));

                Response.Headers[HeaderNames.CacheControl] = "no-cache";
                Response.Headers[HeaderNames.Pragma] = "no-cache";
                Response.ContentLength = buffer.Length;
                Response.ContentType = "application/json;charset=UTF-8";
                Response.StatusCode = 401;

                await Response.Body.WriteAsync(buffer, 0, buffer.Length);
                return await Task.FromResult(AuthenticateResult.Fail(new Exception("Missing Authorization")));
            }

            string token = null;

            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) {
                token = authorization.Substring("Bearer ".Length).Trim();
            }



            if (string.IsNullOrEmpty(token)) {
                return await Task.FromResult(AuthenticateResult.Fail("Incorrect bearer token"));
            }

            //TODO: verify token in SQL SERVER
            //Performance tips: store valid token and minimal user information in memory for next Authentication
            //When user change password, remove the token record from memory

            //if valid

            var identities = new List<ClaimsIdentity> { };

            //var principal = new ClaimsPrincipal(identities);
            var principal = new ClaimsPrincipal(new BearerTokenIdentity());
            principal.AddIdentity(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, "{user id here}"),
                new Claim(ClaimTypes.Surname, "{user surname}"),
                new Claim(ClaimTypes.GivenName, "{user given name}")
            }));

            var ticket = new AuthenticationTicket(principal, Options.Scheme);
            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }


}
