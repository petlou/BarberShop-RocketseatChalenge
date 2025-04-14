using BarberShop.Application.UseCases.Billings;
using BarberShop.Communication.Enums;
using BarberShop.Exception;
using Shouldly;
using Tests.CommonUtilities.Requests;

namespace Tests.Validators.Billings;
public class RegisterNewBillingValidatorTests
{
    [Fact]
    public void ShouldReturnSuccess()
    {
        var validator = new RegisterNewBillingValidator();
        var request = RequestRegisterBillingDTOBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void ShouldReturnErrorWhenTitleIsEmptyOrNull(string? title)
    {
        var validator = new RegisterNewBillingValidator();
        var request = RequestRegisterBillingDTOBuilder.Build();
        request.Title = title;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == ResourceErrorMessages.TITLE_REQUIRED);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ShouldReturnErrorWhenAmountIsLessOrEqualThanZero(decimal amount)
    {
        var validator = new RegisterNewBillingValidator();
        var request = RequestRegisterBillingDTOBuilder.Build();
        request.Amount = amount;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_0);
    }

    [Fact]
    public void ShouldReturnErrorWhenDateIsInFuture()
    {
        var validator = new RegisterNewBillingValidator();
        var request = RequestRegisterBillingDTOBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == ResourceErrorMessages.DATE_MUST_BE_IN_PAST);
    }

    [Fact]
    public void ShouldReturnErrorWhenPaymentTypeIsInvalid()
    {
        var validator = new RegisterNewBillingValidator();
        var request = RequestRegisterBillingDTOBuilder.Build();
        request.PaymentType = (PaymentTypeEnum)99;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == ResourceErrorMessages.PAYMENT_TYPE_INVALID);
    }
}
