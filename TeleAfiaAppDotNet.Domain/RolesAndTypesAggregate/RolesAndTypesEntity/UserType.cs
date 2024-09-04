using System.ComponentModel.DataAnnotations;

namespace TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;

public class UserType
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string TypeName { get; set; }

    public ICollection<PractitionerType> PractitionerTypes { get; set; } = new List<PractitionerType>();
}
