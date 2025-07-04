using System.Net.Mime;
using System.Net;
using System.Text.Json;

namespace FleetManager.WebApi.Middleware
{
    public class ErrorMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger _log = loggerFactory.CreateLogger("Error middleware");

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "An unexpected error occurred: {Message}", ex.Message);
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(HttpContext context, Exception e)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (e is ArgumentException)
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            else if (e is KeyNotFoundException)
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            else
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new
            {
                e.Message,
                ExceptionType = e.GetType().Name,
                context.Response.StatusCode
            };

            var jsonResponse = System.Text.Json.JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
