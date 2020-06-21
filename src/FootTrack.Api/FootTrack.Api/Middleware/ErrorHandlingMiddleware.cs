using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using FootTrack.Api.Exceptions;

namespace FootTrack.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = exception switch
            {
                NotFoundException _ => HttpStatusCode.NotFound,
                ArgumentException _ => HttpStatusCode.BadRequest,
                AlreadyExistsException _ => HttpStatusCode.Conflict,
                _ => HttpStatusCode.InternalServerError
            };

            var result = JsonConvert.SerializeObject(new
            {
                error = exception.Message
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;

            return context.Response.WriteAsync(result);
        }
    }
}
