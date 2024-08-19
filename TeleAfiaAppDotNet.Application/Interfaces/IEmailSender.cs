using TeleAfiaAppDotNet.Domain.Message;

namespace TeleAfiaAppDotNet.Application.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
