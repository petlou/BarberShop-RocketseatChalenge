namespace BarberShop.Application.UseCases.Reports.Excel;
public interface IGenerateBillingReportExcel
{
    Task<byte[]> Execute(DateOnly month);
}
