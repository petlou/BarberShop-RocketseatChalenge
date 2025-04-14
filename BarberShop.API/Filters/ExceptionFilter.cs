using BarberShop.Communication.ResponseDTO.Errors;
using BarberShop.Exception;
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
        var cashFlowException = (BarberShopBaseException)context.Exception;
        var errorResponse = new ResponseErrorDTO(cashFlowException.GetErrors());

        context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }
    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorMessages = new ResponseErrorDTO(ResourceErrorMessages.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorMessages);
    }
}
