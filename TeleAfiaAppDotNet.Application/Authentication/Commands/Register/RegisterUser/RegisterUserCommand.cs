using MediatR;
using TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.RegisterUserDTOs;

namespace TeleAfiaAppDotNet.Application.Authentication.Commands.Register.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisterResponse>
    {
        public UserRegistrationDto User { get; }

        public RegisterUserCommand(UserRegistrationDto user)
        {
            User = user;
        }
    }
}