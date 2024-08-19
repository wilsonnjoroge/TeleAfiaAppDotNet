using MediatR;
using TeleAfiaAppDotNet.Contracts.EmailRequestAndResponse;

namespace TeleAfiaAppDotNet.Application.SendEmail.Command
{
    public class SendEmailCommand : IRequest<SendEmailResponse>
    {
        public SendEmailRequest SendEmailRequest { get; set; }

        public SendEmailCommand(SendEmailRequest request)
        {
            SendEmailRequest = request;
        }
    }
}
