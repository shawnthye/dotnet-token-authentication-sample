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

namespace TokenAuthenticationSample.Security {
    public class BearerTokenOptions : AuthenticationSchemeOptions {

        public const string DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        public string Scheme => DefaultAuthenticateScheme;
        public StringValues AuthKey { get; set; }
    }
}
