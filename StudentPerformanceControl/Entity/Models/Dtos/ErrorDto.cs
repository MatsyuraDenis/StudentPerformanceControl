namespace Entity.Models.Dtos
{
    public class ErrorDto
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public ErrorDto(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}