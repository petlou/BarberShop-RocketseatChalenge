using BarberShop.Application.UseCases.Billings;
using BarberShop.Communication.RequestDTO.Billings;
using BarberShop.Exception.Exceptions;

namespace BarberShop.Application.Utils;
internal static class ValidateBilling
{
    internal static void Run(RequestRegisterOrUpdateBillingDTO request)
    {
        var result = new RegisterNewBillingValidator().Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
