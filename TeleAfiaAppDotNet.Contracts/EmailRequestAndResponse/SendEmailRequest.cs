using System.ComponentModel.DataAnnotations;

namespace TeleAfiaAppDotNet.Contracts.EmailRequestAndResponse
{
    public class SendEmailRequest
    {
        [Required]
        public string To { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
