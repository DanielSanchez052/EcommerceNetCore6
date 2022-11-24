
using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ExcepcionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExcepcionMiddleware> _logger;
        private readonly IHostEnvironment _config;

        public ExcepcionMiddleware(RequestDelegate next, ILogger<ExcepcionMiddleware> logger, IHostEnvironment config)
        {
            _next = next;
            _logger = logger;
            _config = config;
        }

        public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _config.IsDevelopment()
                ? new ApiExcepcion((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                : new ApiResponse((int)HttpStatusCode.InternalServerError);

                var json = JsonSerializer.Serialize((ApiExcepcion)response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}