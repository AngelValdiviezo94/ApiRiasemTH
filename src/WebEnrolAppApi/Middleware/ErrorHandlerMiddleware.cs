using ExceptionsAp = EnrolApp.Application.Common.Exceptions;
using Newtonsoft.Json;

namespace WebEnrolAppApi.Middleware;

public class ErrorHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var response = new
        {
            Message = GetTitle(exception),
            statusCode,
            //detail = "Message:" + exception.Message + " ::InnerException:" + exception.InnerException?.Message ?? string.Empty,
            //errors = GetErrors(exception)
            Data = "",
            Succeeded = false
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        var objResponse = JsonConvert.SerializeObject(response);
        _logger.LogError("ErrorHandlerMiddleware::HandleExceptionAsync:{objResponse} ", objResponse);
        await httpContext.Response.WriteAsync(objResponse);
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            KeyNotFoundException => StatusCodes.Status404NotFound,
            ExceptionsAp.ApiException => StatusCodes.Status400BadRequest,
            ExceptionsAp.NotFoundException => StatusCodes.Status404NotFound,
            ExceptionsAp.ValidationException => 422,//StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            ApplicationException applicationException => applicationException.Message,
            ExceptionsAp.ApiException apiException => apiException.Message,
            ExceptionsAp.ValidationException validateException => validateException.Message,
            _ => "Server Error"
        };

    private static IDictionary<string, string[]> GetErrors(Exception exception)
    {
        IDictionary<string, string[]> errors = new Dictionary<string, string[]>();

        if (exception is ExceptionsAp.ValidationException validationException)
        {
            errors = validationException.Errors;
        }

        return errors;
    }

}
