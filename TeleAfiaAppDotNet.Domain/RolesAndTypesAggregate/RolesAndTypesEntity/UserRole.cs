using System.ComponentModel.DataAnnotations;
using TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities;

namespace TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;

public class UserRole
{
    [Key]
    public Guid Id { get; set; }
    public User User { get; set; }

    public Guid RoleId { get; set; }
    public Role Role { get; set; }
}
