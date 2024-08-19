using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;
using TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities;

namespace TeleAfiaAppDotNet.Application.Authentication.Common
{
    public static class UserRolesAssigner
    {
        public static async Task AssignRolesToUserAsync(IUserRepository userRepository, IRoleRepository roleRepository, User user, List<string> roleNames)
        {
            try
            {
                if (roleNames != null && roleNames.Count > 0)
                {
                    foreach (var roleName in roleNames)
                    {
                        var role = await roleRepository.GetRoleByNameAsync(roleName);
                        if (role == null)
                        {
                            throw new Exception($"Role '{roleName}' not found.");
                        }

                        user.UserRoles.Add(new UserRole
                        {
                            Id = Guid.NewGuid(),
                            User = user,
                            RoleId = role.RoleId,
                            Role = role
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }
    }
}
