using System.ComponentModel.DataAnnotations;

namespace TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;

public class Role
{
    [Key]
    public Guid RoleId { get; set; }

    [Required]
    public string RoleName { get; set; }

    // Navigation property for UserRole
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
