namespace BarberShop.Application.UseCases.Reports.Pdf;
public interface IGenerateBillingReportPdf
{
    Task<byte[]> Execute(DateOnly date);
}
