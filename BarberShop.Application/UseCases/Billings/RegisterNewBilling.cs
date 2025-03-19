using BarberShop.Communication.RequestDTO.Billings;
using BarberShop.Communication.ResponseDTO.Billings;
using BarberShop.Exception.Exceptions;

namespace BarberShop.Application.UseCases.Billings
{
    public class RegisterNewBilling
    {
        public ResponseRegisterBillingDTO Execute(RequestRegisterBillingDTO request)
        {
            Validate(request);

            return new ResponseRegisterBillingDTO();
        }

        private void Validate(RequestRegisterBillingDTO request)
        {
            var result = new RegisterNewBillingValidator().Validate(request);

            if(!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
