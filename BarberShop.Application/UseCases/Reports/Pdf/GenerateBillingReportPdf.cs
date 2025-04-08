
using BarberShop.Domain.Repositories;

namespace BarberShop.Application.UseCases.Reports.Pdf;
public class GenerateBillingReportPdf : IGenerateBillingReportPdf
{
    private const string CURRENCY_SYMBOL = "$";
    private readonly IBillingRepository _repository;

    public GenerateBillingReportPdf(IBillingRepository repository)
    {
        _repository = repository;
    }
    public async Task<byte[]> Execute(DateOnly date)
    {

        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1, hour: 0, minute: 0, second: 0, DateTimeKind.Utc);

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59, DateTimeKind.Utc);

        var billings = await _repository.FilterByMonth(startDate, endDate);

        if (billings.Count == 0)
        {
            return [];
        }

        return [];
    }
}
