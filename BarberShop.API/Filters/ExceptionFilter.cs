using BarberShop.Communication.ResponseDTO.Errors;
using BarberShop.Exception.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarberShop.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BarberShopBaseException)
        {
            HandleProjectException(context);
        } else
        {
            ThrowUnknownError(context);
        }
    }
    private void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException)
        {
            ErrorOnValidationException exception = (ErrorOnValidationException)context.Exception;
            var errorMessages = new ResponseErrorDTO(exception.Errors);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorMessages);
        }
    }
    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorMessages = new ResponseErrorDTO("Unknown Error!");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorMessages);
    }
}
