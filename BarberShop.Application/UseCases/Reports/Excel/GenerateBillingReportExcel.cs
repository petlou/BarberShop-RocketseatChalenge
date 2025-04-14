

using BarberShop.Application.Utils;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Extensions;
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
        var (startDate, endDate) = GetMonthRange.Run(date);
        var billings = await _repository.FilterByMonth(startDate, endDate);

        if (billings.Count == 0)
            return [];

        using var workBook = new XLWorkbook();

        workBook.Author = "BarberShop APP";
        workBook.Style.Font.FontSize = 12;
        workBook.Style.Font.FontName = "Calibri";

        var workSheet = workBook.Worksheets.Add(date.ToString("Y"));        

        InsertHeader(workSheet);

        var row = 2;
        foreach (var billing in billings)
        {
            AddRows(workSheet, billing, row);
            row++;
        }

        var file = new MemoryStream();
        workBook.SaveAs(file);

        return file.ToArray();
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
        workSheet.Cell("D1").Value = ResourceReportMessages.AMOUNT;
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

    private void AddRows(IXLWorksheet workSheet, Billing billing, int row)
    {
        workSheet.Cell($"A{row}").Value = billing.Title;
        workSheet.Cell($"B{row}").Value = billing.Date;
        workSheet.Cell($"C{row}").Value = billing.PaymentType.PaymentTypeEnumToString();

        workSheet.Cell($"D{row}").Value = billing.Amount;
        workSheet.Cell($"D{row}").Style.NumberFormat.Format = $"{CURRENCY_SYMBOL} #,##0.00";

        workSheet.Cell($"E{row}").Value = billing.Description;
    }
}
