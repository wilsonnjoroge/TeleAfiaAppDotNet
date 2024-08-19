using MediatR;
using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Contracts.EmailRequestAndResponse;
using TeleAfiaAppDotNet.Domain.Message;

namespace TeleAfiaAppDotNet.Application.SendEmail.Command
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, SendEmailResponse>
    {
        private readonly IEmailService _emailService;

        public SendEmailCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<SendEmailResponse> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var sendEmailRequest = request.SendEmailRequest;

            var message = new Message(
                new[] { sendEmailRequest.To },
                sendEmailRequest.Subject,
                sendEmailRequest.Body
            );

            try
            {
                _emailService.SendEmail(message);
                return new SendEmailResponse
                {
                    Message = "Email sent successfully",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new SendEmailResponse
                {
                    Message = "Failed to send email",
                    Success = false,
                    Error = ex.Message
                };
            }
        }
    }
}
