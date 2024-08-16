using MediatR;
using TeleAfiaAppDotNet.Application.Interfaces.UserRoleAndTypeRepositories;
using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.UserRoleDTOs;
using TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;

namespace TeleAfiaAppDotNet.Application.UserRoleManagement.Commands.AddRole;

public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, UserRoleResponse>
{
    private readonly IRoleRepository _roleRepository;

    public AddRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<UserRoleResponse> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var role = await _roleRepository.GetRoleByNameAsync(request.UserRoleRequest.RoleName);
            if (role != null)
            {
                throw new Exception($"Role with the Name '{request.UserRoleRequest.RoleName}' already exists");
            }

            var roleToAdd = new Role
            {
                RoleName = request.UserRoleRequest.RoleName,

            };

            await _roleRepository.AddRoleAsync(roleToAdd);
            var addedRole = await _roleRepository.GetRoleByNameAsync(request.UserRoleRequest.RoleName);

            var Id = addedRole.RoleId;

            var roleResponse = new UserRoleResponse
            {
                Message = "Role Added Successfully",
                RoleId = Id,
                RoleName = roleToAdd.RoleName
            };

            return roleResponse;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.ToString());
        }
    }
}
