using System.Globalization;

namespace BarberShop.API.Middlewares;

public class CultureMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        var requestCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        if (string.IsNullOrWhiteSpace(requestCulture) || !supportedCultures.Exists(lang => lang.Name.Equals(requestCulture)))
        {
            requestCulture = "en-US";
        }

        CultureInfo.CurrentCulture = new CultureInfo(requestCulture);
        CultureInfo.CurrentUICulture = new CultureInfo(requestCulture);

        await _next(context);
    }
}
