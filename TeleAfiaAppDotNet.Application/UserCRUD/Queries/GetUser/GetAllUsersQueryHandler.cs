using MediatR;
using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities;

namespace TeleAfiaAppDotNet.Application.UserCRUD.Queries.GetUser
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();

                return users;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }
    }
}
