using BarberShop.Domain.Enums;
using BarberShop.Domain.Reports;

namespace BarberShop.Domain.Extensions;
public static class PaymentTypeEnumExtension
{
    public static string PaymentTypeEnumToString(this PaymentTypeEnum paymentType)
    {
        return paymentType switch
        {
            PaymentTypeEnum.CASH => ResourceReportMessages.CASH,
            PaymentTypeEnum.CREDIT_CARD => ResourceReportMessages.CREDIT_CARD,
            _ => string.Empty
        };
    }
}
