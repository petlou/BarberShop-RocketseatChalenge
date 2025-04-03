using System.Net;

namespace BarberShop.Exception.Exceptions;
internal class NotFoundException : BarberShopBaseException
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public NotFoundException(string message) : base(message) { }

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
