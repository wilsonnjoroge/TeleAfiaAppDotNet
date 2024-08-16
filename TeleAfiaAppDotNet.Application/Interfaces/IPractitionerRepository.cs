using TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities;

namespace TeleAfiaAppDotNet.Application.Interfaces
{
    public interface IPractitionerRepository
    {
        Task<Practitioner> GetPractitionerByIdAsync(Guid id);
        Task UpdatePractitionerAsync(Practitioner practitioner);
    }
}
