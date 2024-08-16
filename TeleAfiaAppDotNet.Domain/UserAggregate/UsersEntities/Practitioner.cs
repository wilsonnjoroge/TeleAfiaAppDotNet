using TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;

namespace TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities
{
    public class Practitioner : User
    {
        public string LicenseNumber { get; set; }
        public ICollection<PractitionerType> PractitionerTypes { get; set; } = new List<PractitionerType>();
    }

}
