using MediatR;
using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.ResetPasswordDTOs;

namespace TeleAfiaAppDotNet.Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>
    {
        private readonly IUserRepository _userRepository;
        public ResetPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResetPasswordResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Request = request.ResetPasswordRequest;

                var user = await _userRepository.GetUserByEmailAsync(Request.Email);

                if (user == null)
                {
                    throw new UnauthorizedAccessException("User not found");
                }

                var isResetCodeValid = BCrypt.Net.BCrypt.Verify(Request.ResetCode, user.ResetToken);

                if (!isResetCodeValid)
                {
                    throw new UnauthorizedAccessException("Reset code is not valid");
                }

                var newPassword = Request.NewPassword;
                user.ChangePassword(newPassword);

                user.ClearResetToken(user);

                await _userRepository.UpdateUserAsync(user);

                var response = new ResetPasswordResponse
                {
                    Message = "Password reset successfully"
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }
    }
}
