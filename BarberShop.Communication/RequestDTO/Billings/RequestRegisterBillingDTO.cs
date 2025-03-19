using BarberShop.Communication.Enums;

namespace BarberShop.Communication.RequestDTO.Billings
{
    public class RequestRegisterBillingDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public PaymentTypeEnum PaymentType { get; set; }
    }
}
