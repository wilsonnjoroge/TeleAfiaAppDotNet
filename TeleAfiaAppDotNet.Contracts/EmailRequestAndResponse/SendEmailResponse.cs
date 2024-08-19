namespace TeleAfiaAppDotNet.Contracts.EmailRequestAndResponse
{
    public class SendEmailResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
