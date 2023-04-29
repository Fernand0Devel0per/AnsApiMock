using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockAbiANS.Middlewares
{
  public class SimpleTokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _expectedToken = "MinhaStringLocalDeExemplo12345";

        public SimpleTokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string authorizationHeader = context.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token de autorização ausente ou inválido.");
                return;
            }

            string token = authorizationHeader.Substring("Bearer ".Length).Trim();

            if (token != _expectedToken)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Token de autorização não corresponde ao esperado.");
                return;
            }

            await _next(context);
        }
    }
}