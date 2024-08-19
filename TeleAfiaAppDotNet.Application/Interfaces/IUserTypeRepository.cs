using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.UserTypeDTOs;
using TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;

namespace TeleAfiaAppDotNet.Application.Interfaces
{
    public interface IUserTypeRepository
    {
        Task<List<UserType>> GetAllUserTypesAsync();
        Task<UserType> GetUserTypeByNameAsync(string userTypeName);
        Task<UserType> GetUserTypeByIdAsync(Guid id);
        Task<Guid> AddUserTypeAsync(UserType userType);
        Task<bool> UpdateUserTypeAsync(Guid userTypeId, UserTypeRequest userTypeDto);
        Task<bool> DeleteUserTypeAsync(Guid userTypeId);
    }
}
