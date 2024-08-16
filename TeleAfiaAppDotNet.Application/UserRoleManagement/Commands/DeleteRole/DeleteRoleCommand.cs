using MediatR;
using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.UserRoleDTOs;

namespace TeleAfiaAppDotNet.Application.UserRoleManagement.Commands.DeleteRole;

public class DeleteRoleCommand : IRequest<UserRoleResponse>
{
    public Guid RoleId { get; set; }
    public DeleteRoleCommand(Guid roleId)
    {
        RoleId = roleId;
    }
}
