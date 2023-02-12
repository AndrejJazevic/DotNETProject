using Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;
        public ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment environment, ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
            _environment = environment;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                context.Response.ContentType = "application/json";

                switch (error)
                {
                    case ConflictException e:
                        context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    case NotEnoughCreditsException e:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case UserDoesNotHaveAccessException e:
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    case NotFoundException e:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await context.Response.WriteAsync(result);
                LogErrorExceptionWithRequestBody(context, error);
            }
        }

        private void LogErrorExceptionWithRequestBody(HttpContext context, Exception exception)
        {
            _logger.LogError($"Exception thrown: {exception} Method: {context.Request.Method}, Content: {context.Request.GetDisplayUrl()} Body: {JsonSerializer.Serialize(context.Request.Body).ToString()}");
        }
    }
}