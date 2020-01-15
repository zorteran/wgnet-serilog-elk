using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeLogging
{
    class LogEnrichmentMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogEnrichmentMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                      .CreateLogger<LogEnrichmentMiddleware>();
        }
        public async Task Invoke(HttpContext context)
        {
            var user = new
            {
                IsAuthenticated = context.User.Identity.IsAuthenticated,
                AuthenticationType = context.User.Identity.AuthenticationType,
                UserName = context.User.Identity.Name,
                Claims = context.User.Claims.Select(o => o.Value)
            };
            _logger.BeginScope(new Dictionary<string, object> {
                //{ "CorrelationId", Guid.NewGuid().ToString() },
                //{ "MachineName",Environment.MachineName},
                { "RequestMethod", context.Request.Method },
                { "RequestQuery", context.Request.Query},
                { "RequestQueryString", context.Request.QueryString },
                { "RequestPath", context.Request.Path },
                { "RequestPathBase", context.Request.PathBase },
                { "RequestContentLength", context.Request.ContentLength },
                { "RequestContentType", context.Request.ContentType },
                { "User", user }
            });
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Request Exception :-(");
                throw;
            }
            finally
            {
                sw.Stop();
                _logger.BeginScope(new Dictionary<string, object> {
                { "ElapsedMiliseconds", sw.ElapsedMilliseconds },
                { "StatusCode", context.Response.StatusCode }
            });
                _logger.LogInformation("Request completed");
            }
        }
    }


    public static class LogEnrichmentMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogEnrichment(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogEnrichmentMiddleware>();
        }
    }

}
