using MediatR;
using TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;

namespace TeleAfiaAppDotNet.Application.UserRoleManagement.Queries.GetRoles
{
    public class GetRoleQuery : IRequest<List<Role>>
    {
    }
}
