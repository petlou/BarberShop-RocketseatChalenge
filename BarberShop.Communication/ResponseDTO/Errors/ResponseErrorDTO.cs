namespace BarberShop.Communication.ResponseDTO.Errors;
public class ResponseErrorDTO
{
    public List<string> ErrorMessages { get; set; } = [];

    public ResponseErrorDTO(string messages)
    {
        ErrorMessages = [messages];
    }

    public ResponseErrorDTO(List<string> messages)
    {
        ErrorMessages = messages;
    }
}
