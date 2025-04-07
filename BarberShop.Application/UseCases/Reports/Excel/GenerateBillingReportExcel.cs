

using BarberShop.Domain.Enums;
using BarberShop.Domain.Reports;
using BarberShop.Domain.Repositories;
using ClosedXML.Excel;

namespace BarberShop.Application.UseCases.Reports.Excel;
public class GenerateBillingReportExcel : IGenerateBillingReportExcel
{
    private const string CURRENCY_SYMBOL = "$";
    private readonly IBillingRepository _repository;

    public GenerateBillingReportExcel(IBillingRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1, hour: 0, minute: 0, second: 0, DateTimeKind.Utc);

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59,DateTimeKind.Utc);

        var billings = await _repository.FilterByMonth(startDate.ToUniversalTime(), endDate.ToUniversalTime());

        if (billings.Count == 0)
        {
            return [];
        }

        using var workBook = new XLWorkbook();

        workBook.Author = "Peter Lourenço";
        workBook.Style.Font.FontSize = 12;
        workBook.Style.Font.FontName = "Calibri";

        var workSheet = workBook.Worksheets.Add(date.ToString("Y"));        

        InsertHeader(workSheet);

        var raw = 2;
        foreach (var billing in billings)
        {
            workSheet.Cell($"A{raw}").Value = billing.Title;
            workSheet.Cell($"B{raw}").Value = billing.Date;
            workSheet.Cell($"C{raw}").Value = ConvertPaymentType(billing.PaymentType);

            workSheet.Cell($"D{raw}").Value = billing.Amount;
            workSheet.Cell($"D{raw}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #,##0.00";

            workSheet.Cell($"E{raw}").Value = billing.Description;

            raw++;
        }

        var file = new MemoryStream();
        workBook.SaveAs(file);

        return file.ToArray();
    }

    private string ConvertPaymentType(PaymentTypeEnum payment)
    {
        return payment switch
        {
            PaymentTypeEnum.CASH => ResourceReportMessages.CASH,
            PaymentTypeEnum.CREDIT_CARD => ResourceReportMessages.CREDIT_CARD,
            _ => string.Empty
        };
    }

    private void InsertHeader(IXLWorksheet workSheet)
    {
        workSheet.Columns("A", "D").Width = 20;
        workSheet.Columns("E").Width = 50;

        workSheet.Column("A").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        workSheet.Column("B").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Column("C").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Column("D").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        workSheet.Column("E").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

        workSheet.Cell("A1").Value = ResourceReportMessages.TITLE;
        workSheet.Cell("B1").Value = ResourceReportMessages.DATE;
        workSheet.Cell("C1").Value = ResourceReportMessages.PAYMENT_TYPE;
        workSheet.Cell("D1").Value = ResourceReportMessages.VALUE;
        workSheet.Cell("E1").Value = ResourceReportMessages.DESCRIPTION;

        workSheet.Cells("A1:E1").Style.Font.Bold = true;
        workSheet.Cells("A1:E1").Style.Font.FontColor = XLColor.FromHtml("#FFFFFF");

        workSheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#205858");

        workSheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
