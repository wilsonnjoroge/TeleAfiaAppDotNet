using Microsoft.EntityFrameworkCore;
using TeleAfiaAppDotNet.Application.Interfaces.UserRoleAndTypeRepositories;
using TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;
using TeleAfiaAppDotNet.Infrastructure.Data;

namespace TeleAfiaAppDotNet.Infrastructure.Repositories.UserRoleAndTypeManagement;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Role>> GetAllRoles()
    {
        try
        {
            return await _context.Roles.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.ToString());
        }
    }

    // Method to fetch a role by its ID
    public async Task<Role> GetRoleByIdAsync(Guid roleId)
    {
        try
        {
            return await _context.Roles.FindAsync(roleId);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.ToString());
        }
    }

    // Method to fetch a role by its name
    public async Task<Role> GetRoleByNameAsync(string roleName)
    {
        try
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == roleName);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.ToString());
        }
    }

    // Method to add a new role
    public async Task<Guid> AddRoleAsync(Role role)
    {
        try
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return role.RoleId;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.ToString());
        }
    }

    public async Task<Role> UpdateRoleAsync(Guid roleId, string newRoleName)
    {
        try
        {

            var role = await _context.Roles.FindAsync(roleId);
            if (role == null)
            {
                throw new Exception($"Role with ID '{roleId}' does not exist.");
            }

            role.RoleName = newRoleName;
            await _context.SaveChangesAsync();

            return role;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.ToString());
        }
    }


    // Method to delete a role by ID
    public async Task DeleteRoleAsync(Guid roleId)
    {
        try
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.ToString());
        }
    }
}
