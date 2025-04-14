using BarberShop.Application.AutoMapper;
using BarberShop.Application.UseCases.Billings.Delete;
using BarberShop.Application.UseCases.Billings.GetAll;
using BarberShop.Application.UseCases.Billings.GetOne;
using BarberShop.Application.UseCases.Billings.Register;
using BarberShop.Application.UseCases.Billings.Update;
using BarberShop.Application.UseCases.Reports.Excel;
using BarberShop.Application.UseCases.Reports.Pdf;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Application.Extensions;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterNewBilling, RegisterNewBilling>();
        services.AddScoped<IGetAllBillings, GetAllBillings>();
        services.AddScoped<IGetOneBilling, GetOneBilling>();
        services.AddScoped<IUpdateBilling, UpdateBilling>();
        services.AddScoped<IDeleteBilling, DeleteBilling>();

        services.AddScoped<IGenerateBillingReportExcel, GenerateBillingReportExcel>();
        services.AddScoped<IGenerateBillingReportPdf, GenerateBillingReportPdf>();
    }
}
