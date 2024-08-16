using MediatR;
using TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities;

namespace TeleAfiaAppDotNet.Application.UserCRUD.Queries.GetUser
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {
    }
}
