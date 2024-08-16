namespace TeleAfiaAppDotNet.Application.Errors
{
    public class Errors
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public Errors(int statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
