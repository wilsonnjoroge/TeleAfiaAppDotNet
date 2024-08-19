using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.PractitionerTypeDTOs;

namespace TeleAfiaAppDotNet.Application.Interfaces
{
    public interface IPractitionerTypeRepository
    {
        Task<List<PractitionerTypeRequest>> GetAllPractitionerTypesAsync();
        Task<PractitionerTypeRequest> GetPractitionerTypeByIdAsync(Guid practitionerTypeId);
        Task<Guid> AddPractitionerTypeAsync(PractitionerTypeRequest practitionerTypeDto);
        Task<bool> UpdatePractitionerTypeAsync(Guid practitionerTypeId, PractitionerTypeRequest practitionerTypeDto);
        Task<bool> DeletePractitionerTypeAsync(Guid practitionerTypeId);
    }
}
