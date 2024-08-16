using MediatR;
using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Contracts.UserCrudDTOs.GetUserDTOs;

namespace TeleAfiaAppDotNet.Application.UserCRUD.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Request = request.GetUserRequest;

                var user = await _userRepository.GetUserByEmailOrIdNumberAsync(Request.Email, Request.IdNumber);
                if (user == null)
                {
                    throw new ApplicationException("User not found");
                }

                var response = new GetUserResponse
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    IdNumber = user.IdNumber,
                    Location = user.Location,
                    DateOfBirth = user.DateOfBirth,
                    UserType = user.UserType
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
