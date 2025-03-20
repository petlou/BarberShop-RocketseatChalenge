using BarberShop.Communication.RequestDTO.Billings;
using BarberShop.Exception;
using FluentValidation;

namespace BarberShop.Application.UseCases.Billings
{
    public class RegisterNewBillingValidator : AbstractValidator<RequestRegisterBillingDTO>
    {
        public RegisterNewBillingValidator()
        {
            RuleFor(b => b.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED);
            RuleFor(b => b.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_0);
            RuleFor(b => b.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.DATE_MUST_BE_GREATER_OR_EQUAL_TODAY);
            RuleFor(b => b.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.DATE_MUST_BE_GREATER_OR_EQUAL_TODAY);
        }
    }
}
