namespace BarberShop.Exception.Exceptions;

public abstract class BarberShopBaseException : SystemException
{
    protected BarberShopBaseException(string message) : base(message) { }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
