using MediatR;
using TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.RegisterUserDTOs;
using TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities;
using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Application.Authentication.Common;
using TeleAfiaAppDotNet.Domain.Message;


namespace TeleAfiaAppDotNet.Application.Authentication.Commands.Register.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IEmailService _emailService;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IJwtTokenGenerator jwtTokenGenerator,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _emailService = emailService;
        }

        public async Task<RegisterResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userDto = request.User;

                // Check if user exists in the database
                var existingUser = await _userRepository.UserExists(userDto.Email);

                if (existingUser)
                {
                    throw new ApplicationException($"User with email: '{userDto.Email}' already exists");
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    PhoneNumber = userDto.PhoneNumber,
                    IdNumber = userDto.IdNumber,
                    Location = userDto.Location,
                    DateOfBirth = userDto.DateOfBirth,
                    Password = hashedPassword,
                    UserType = userDto.UserType,
                    LicenseNumber = userDto.LicenseNumber,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                // Assign roles to the user
                await UserRolesAssigner.AssignRolesToUserAsync(_userRepository, _roleRepository, user, userDto.UserRoles);

                // Save user to repository
                await _userRepository.AddUserAsync(user);

                var message = new Message(new[] { userDto.Email }, "Registration", "Welcome to teleafia platform");

                _emailService.SendEmail(message);

                // Create the response
                var response = new RegisterResponse
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    IdNumber = user.IdNumber,
                    Location = user.Location,
                    UserRoles = userDto.UserRoles,
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
