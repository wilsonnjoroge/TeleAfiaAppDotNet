using MediatR;
using TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.LoginDTOs;

namespace TeleAfiaAppDotNet.Application.Authentication.Queries.LogIn
{
    public class LoginQuery : IRequest<LoginResponse>
    {
        public LoginRequest LoginRequest { get; set; }

        public LoginQuery(LoginRequest user)
        {
            LoginRequest = user;
        }
    }
}
