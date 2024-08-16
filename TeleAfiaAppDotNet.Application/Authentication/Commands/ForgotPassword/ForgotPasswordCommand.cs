using MediatR;
using TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.ForgotPasswordDTOs;

namespace TeleAfiaAppDotNet.Application.Authentication.Commands.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<ForgotPasswordResponse>
    {
        public ForgotPasswordRequest ForgotPasswordRequest { get; set; }
        public ForgotPasswordCommand(ForgotPasswordRequest user)
        {
            ForgotPasswordRequest = user;
        }
    }
}
