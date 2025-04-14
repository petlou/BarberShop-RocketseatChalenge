
using System.Reflection;
using BarberShop.Application.UseCases.Reports.Pdf.Colors;
using BarberShop.Application.UseCases.Reports.Pdf.Fonts;
using BarberShop.Application.Utils;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Extensions;
using BarberShop.Domain.Reports;
using BarberShop.Domain.Repositories;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using Font = MigraDoc.DocumentObjectModel.Font;

namespace BarberShop.Application.UseCases.Reports.Pdf;
public class GenerateBillingReportPdf : IGenerateBillingReportPdf
{
    private readonly IBillingRepository _repository;
    private const string CURRENCY_SYMBOL = "$";
    private const int HEIGHT_ROW_BILLING_TABLE = 25;
    private const int INDENT_SPACE = 10;

    public GenerateBillingReportPdf(IBillingRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new BillingReportFontResolver();
    }
    public async Task<byte[]> Execute(DateOnly date)
    {
        var (startDate, endDate) = GetMonthRange.Run(date);
        var billings = await _repository.FilterByMonth(startDate, endDate);

        if (billings.Count == 0)
            return [];

        var totalAmount = billings.Sum(expense => expense.Amount);

        var document = CreateDocument(date);
        var page = CreatePage(document);

        CreateHeaderWithLogo(page);
        CreateTotalSpentSection(page, date, totalAmount);

        foreach(var billing in billings)
            AddBillingTable(page, billing);

        return RenderDocument(document);
    }

    private Document CreateDocument(DateOnly date)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportMessages.BILLINGS_FOR} {date:Y}";
        document.Info.Subject = ResourceReportMessages.BILLING_REPORT;
        document.Info.Author = "BarberShop APP";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;

        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private void CreateHeaderWithLogo (Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var imagePath = Path.Combine(directoryName!, "Images", "logo.png");

        var row = table.AddRow();
        row.Cells[0].AddImage(imagePath);

        row.Cells[1].AddParagraph("BarberShop");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 20 };
        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
    }

    private void CreateTotalSpentSection (Section page, DateOnly date, decimal totalAmount)
    {
        var paragraph = page.AddParagraph();

        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        var title = string.Format(ResourceReportMessages.TOTAL_BILLED_IN, date.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });

        paragraph.AddLineBreak();
        paragraph.AddLineBreak();

        
        paragraph.AddFormattedText($"{totalAmount} {CURRENCY_SYMBOL}", new Font { Name = FontHelper.RALEWAY_BLACK, Size = 50 });
    }

    private void AddBillingTable(Section page, Billing billing)
    {
        var table = CreateBillingTable(page);

        var row = table.AddRow();
        row.Height = HEIGHT_ROW_BILLING_TABLE;

        AddBillingTitle(row.Cells[0], billing.Title);
        AddHeaderForAmount(row.Cells[3]);

        row = table.AddRow();
        row.Height = HEIGHT_ROW_BILLING_TABLE;

        row.Cells[0].AddParagraph(billing.Date.ToString("D"));
        SetStyleBaseForExpenseInformation(row.Cells[0]);
        row.Cells[0].Format.LeftIndent = INDENT_SPACE;

        row.Cells[1].AddParagraph(billing.Date.ToString("t"));
        SetStyleBaseForExpenseInformation(row.Cells[1]);

        row.Cells[2].AddParagraph(billing.PaymentType.PaymentTypeEnumToString());
        SetStyleBaseForExpenseInformation(row.Cells[2]);

        AddAmountForExpense(row.Cells[3], billing.Amount);

        if (!string.IsNullOrWhiteSpace(billing.Description))
        {
            row = table.AddRow();
            row.Height = HEIGHT_ROW_BILLING_TABLE;

            row.Cells[0].AddParagraph(billing.Description);
            row.Cells[0].Format.Font = new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
            row.Cells[0].Shading.Color = ColorsHelper.GREEN_ULTRA_LIGHT;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[0].MergeRight = 2;
            row.Cells[0].Format.LeftIndent = INDENT_SPACE;
        }

        AddWhiteSpace(table);
    }

    private Table CreateBillingTable(Section page)
    {
        var table = page.AddTable();
        table.AddColumn("220").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("70").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("125").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("100").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }

    private void AddBillingTitle(Cell cell, string title)
    {
        cell.AddParagraph(title);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = INDENT_SPACE;
    }

    private void AddHeaderForAmount(Cell cell)
    {
        cell.AddParagraph(ResourceReportMessages.AMOUNT);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.GREEN_MEDIUM;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.RightIndent = INDENT_SPACE;
    }

    private void SetStyleBaseForExpenseInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.GREEN_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddAmountForExpense(Cell cell, decimal amount)
    {
        cell.AddParagraph($"{amount} {CURRENCY_SYMBOL}");
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.RightIndent = INDENT_SPACE;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();
        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
}
