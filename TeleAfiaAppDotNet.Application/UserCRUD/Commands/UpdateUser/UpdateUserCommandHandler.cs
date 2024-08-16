using MediatR;
using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Contracts.UserCrudDTOs.UpdateUserDTOs;

namespace TeleAfiaAppDotNet.Application.UserCRUD.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Handle cases where idNumber or email might be null or empty
                var user = await _userRepository.GetUserByEmailOrIdNumberAsync(request.Email, request.IdNumber);

                if (user == null)
                {
                    throw new Exception("User not found!!");
                }

                // Update user information
                user.UpdateUserInfo(
                    request.UpdateUserRequest.FirstName,
                    request.UpdateUserRequest.LastName,
                    user.Email,
                    request.UpdateUserRequest.PhoneNumber,
                    request.UpdateUserRequest.Location,
                    user.DateOfBirth 
                );

                await _userRepository.UpdateUserAsync(user);

                var response = new UpdateUserResponse
                {
                    Message = "User updated successfully",
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Location = user.Location
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
