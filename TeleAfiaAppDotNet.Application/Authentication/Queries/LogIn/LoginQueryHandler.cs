using MediatR;
using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.LoginDTOs;

namespace TeleAfiaAppDotNet.Application.Authentication.Queries.LogIn
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loginRequest = request.LoginRequest;

                // Asynchronously fetch the user by email
                var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);
                if (user == null || user != null && user.IsDeleted is true)
                {
                    throw new UnauthorizedAccessException("User not found");
                }

                // Verify the password using BCrypt
                var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);
                if (!isPasswordValid)
                {
                    throw new UnauthorizedAccessException("Wrong Password");
                }

                // Generate JWT token
                var token = _jwtTokenGenerator.GenerateToken(user);

                // Return authentication response
                return new LoginResponse
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }
    }
}
