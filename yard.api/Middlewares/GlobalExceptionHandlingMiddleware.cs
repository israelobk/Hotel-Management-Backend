using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace yard.api.middlewares
{
    public class GlobalExceptionHandlingMiddleware: IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                // _logger.LogError("Middleware error log");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var problem = new
                {
                    Success = false,
                    Status = (int)HttpStatusCode.InternalServerError,
                    e.Message,
                    e.StackTrace
                };

                string json = JsonSerializer.Serialize(problem);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}