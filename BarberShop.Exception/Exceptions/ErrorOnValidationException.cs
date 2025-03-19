namespace BarberShop.Exception.Exceptions;
public class ErrorOnValidationException : BarberShopBaseException
{
    public List<string> Errors { get; set; }

    public ErrorOnValidationException(List<string> errorMessages)
    {
        Errors = errorMessages;
    }
}
