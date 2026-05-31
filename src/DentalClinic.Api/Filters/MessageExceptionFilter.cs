using DentalClinic.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DentalClinic.Api.Filters;

public class MessageExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not MessageException exception)
            return;

        context.Result = exception switch
        {
            ResourceNotFoundException =>
                new NotFoundObjectResult(exception.Message),

            BadRequestResponseException =>
                new BadRequestObjectResult(exception.Message),

            UnauthorizedResponseException =>
                new UnauthorizedObjectResult(exception.Message),

            _ => null
        };

        context.ExceptionHandled = true;
    }

}
