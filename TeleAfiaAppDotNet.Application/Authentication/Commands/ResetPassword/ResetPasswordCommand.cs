using MediatR;
using TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.ResetPasswordDTOs;

namespace TeleAfiaAppDotNet.Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<ResetPasswordResponse>
    {
        public ResetPasswordRequest ResetPasswordRequest { get; set; }
        public ResetPasswordCommand(ResetPasswordRequest request)
        {
            ResetPasswordRequest = request;
        }
    }
}
