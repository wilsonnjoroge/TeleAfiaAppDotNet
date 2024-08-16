using MediatR;
using TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;

namespace TeleAfiaAppDotNet.Application.UserTypesManagement.Queries.GetUserTypes
{
    public class GetUserTypeQuery : IRequest<List<UserType>>
    {
    }
}
