using BarberShop.Communication.Enums;

namespace BarberShop.Communication.ResponseDTO.Billings;
public class ResponseBillingDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentTypeEnum PaymentType { get; set; }
}
