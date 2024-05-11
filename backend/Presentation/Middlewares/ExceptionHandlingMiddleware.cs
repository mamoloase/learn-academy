
using System.Text.Json;
using Application.Exceptions;
using Domain.Models.Responses;

namespace Presentation.Middlewares;
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;


    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = new ResponseException
        {
            Status = false,
            Message = exception.Message,
            Exceotions = exception is ValidationException validationException ?
                validationException.Errors : null
        };

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status400BadRequest,
            BadRequestException => StatusCodes.Status400BadRequest,
            AccessDeniedException => StatusCodes.Status406NotAcceptable,
            UnauthorizedException => StatusCodes.Status401Unauthorized,

            _ => StatusCodes.Status500InternalServerError
        };
}