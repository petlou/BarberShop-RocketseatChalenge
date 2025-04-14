using BarberShop.Communication.Enums;
using BarberShop.Communication.RequestDTO.Billings;
using Bogus;

namespace Tests.CommonUtilities.Requests;
public class RequestRegisterBillingDTOBuilder
{
    public static RequestRegisterOrUpdateBillingDTO Build()
    {
        var faker = new Faker();

        return new RequestRegisterOrUpdateBillingDTO
        {
            Title = faker.Commerce.ProductName(),
            Description = faker.Commerce.ProductDescription(),
            Date = faker.Date.Past(),
            Amount = faker.Finance.Amount(min: 1, max: 1000),
            PaymentType = faker.PickRandom<PaymentTypeEnum>()
        };
    }
}
