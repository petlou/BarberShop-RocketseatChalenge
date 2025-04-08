using System.Net.Mime;
using BarberShop.Application.UseCases.Reports.Excel;
using BarberShop.Application.UseCases.Reports.Pdf;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers;
public class ReportsController : BarberShopBaseController
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GenerateExcelReport(
        [FromServices] IGenerateBillingReportExcel useCase,
        [FromQuery] DateOnly month)
    {
        byte[] file = await useCase.Execute(month);

        if(file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");

        return NoContent();
    }

    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GeneratePdfReport(
        [FromServices] IGenerateBillingReportPdf useCase,
        [FromQuery] DateOnly month)
    {
        byte[] file = await useCase.Execute(month);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");

        return NoContent();
    }
}
