using MediatR;
using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.UserRoleDTOs;

namespace TeleAfiaAppDotNet.Application.UserRoleManagement.Commands.AddRole;

public class AddRoleCommand : IRequest<UserRoleResponse>
{
    public UserRoleRequest UserRoleRequest { get; set; }
    public AddRoleCommand(UserRoleRequest userRoleRequest)
    {
        UserRoleRequest = userRoleRequest;
    }
}
