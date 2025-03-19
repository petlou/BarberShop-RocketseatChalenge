using BarberShop.Communication.RequestDTO.Billings;
using FluentValidation;

namespace BarberShop.Application.UseCases.Billings
{
    public class RegisterNewBillingValidator : AbstractValidator<RequestRegisterBillingDTO>
    {
        public RegisterNewBillingValidator()
        {
            RuleFor(b => b.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(b => b.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
            RuleFor(b => b.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date must be less than or equal to today");
            RuleFor(b => b.PaymentType).IsInEnum().WithMessage("Payment type is invalid");
        }
    }
}
