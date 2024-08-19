using MediatR;
using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.UserRoleDTOs;

namespace TeleAfiaAppDotNet.Application.UserRoleManagement.Commands.UpdateRole;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, UserRoleResponse>
{
    private readonly IRoleRepository _roleRepository;

    public UpdateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<UserRoleResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingRole = await _roleRepository.GetRoleByIdAsync(request.RoleId);
            if (existingRole == null)
            {
                throw new Exception($"Role with ID '{request.RoleId}' does not exist.");
            }

            var updatedRole = await _roleRepository.UpdateRoleAsync(request.RoleId, request.NewRoleName);

            var response = new UserRoleResponse
            {
                Message = "Role updated successfully",
                RoleName = updatedRole.RoleName
            };

            return response;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.ToString());
        }
    }
}
