using System.Net;
using AikoLearning.Core.Domain.Exceptions;
using AikoLearning.Presentation.WebAPI.Response;
using Newtonsoft.Json;

namespace AikoLearning.Presentation.Middlewares;

public class GlobalErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlerMiddleware(RequestDelegate next)
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

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;        

        switch (exception)
        {
            case DomainValidationException:
                code = HttpStatusCode.UnprocessableEntity;                
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
            case BadRequestException:
                code = HttpStatusCode.BadRequest;                
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        var response = BaseResponseAPI.Create(GetInnerExceptionMessages(exception), false);
        var json = JsonConvert.SerializeObject(response);

        return context.Response.WriteAsync(json);
    }

    private string GetInnerExceptionMessages(Exception ex)
    {
        var messages = ex.Message;
        var innerException = ex.InnerException;
        
        while (innerException != null)
        {
            messages += " --> " + innerException.Message;
            innerException = innerException.InnerException;
        }

        return messages;
    }
}

public static class GlobalErrorHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalErrorHandlerMiddleware>();
    }
}