namespace BarberShop.Communication.ResponseDTO.Billings;
public class ResponseShortBillingDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
