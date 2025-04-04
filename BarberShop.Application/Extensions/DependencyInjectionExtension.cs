using BarberShop.Application.AutoMapper;
using BarberShop.Application.UseCases.Billings.Delete;
using BarberShop.Application.UseCases.Billings.GetAll;
using BarberShop.Application.UseCases.Billings.GetOne;
using BarberShop.Application.UseCases.Billings.Register;
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
        services.AddScoped<IDeleteBilling, DeleteBilling>();
    }
}
