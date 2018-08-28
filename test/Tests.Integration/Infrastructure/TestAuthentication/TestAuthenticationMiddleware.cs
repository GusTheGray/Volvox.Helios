using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;

namespace Tests.Integration.Infrastructure.TestAuthentication
{
    public class TestAuthenticationMiddleware : AuthenticationMiddleware<TestAuthenticationOptions>
    {
        private readonly RequestDelegate next;

        public TestAuthenticationMiddleware(RequestDelegate next, IOptions<TestAuthenticationOptions> options, ILoggerFactory loggerFactory)
          //  : base(next, options, loggerFactory, UrlEncoder.Default) //base only takes next and IAuthenticationSchemeProvider
          : base(next, new TestAuthenticationOptions())
        {
            this.next = next;
        }

        protected override void AuthenticationHandler<TestAuthenticationOptions> CreateHandler()
        {
            return new TestAuthenticationHandler();
        }
    }
}
