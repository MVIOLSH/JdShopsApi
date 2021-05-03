using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace JdShops.Middleware
{
    public class RequestTimeMiddleware :IMiddleware
    {
        private Stopwatch _stpoWatch;
        private readonly ILogger<RequestTimeMiddleware> _logger;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger = logger;
            _stpoWatch = new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stpoWatch.Start();
            await next.Invoke(context);
            _stpoWatch.Stop();

            var elapsedMilliseconds = _stpoWatch.ElapsedMilliseconds;
            if (elapsedMilliseconds / 1000 > 4)
            {
                var message =
                    $"Request [{context.Request.Method}] at {context.Request.Path} took {elapsedMilliseconds}ms";
                _logger.LogInformation(message);
            }
        }
    }
}
