using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeleAfiaAppDotNet.Application.Interfaces.UserRoleAndTypeRepositories;
using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.UserTypeDTOs;
using TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;
using TeleAfiaAppDotNet.Infrastructure.Data;

namespace TeleAfiaAppDotNet.Infrastructure.Repositories.UserRoleAndTypeManagement
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserTypeRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UserType>> GetAllUserTypesAsync()
        {
            try
            {
                var userTypes = await _dbContext.UserTypes
                    .ToListAsync();

                return userTypes;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<UserType> GetUserTypeByNameAsync(string userTypeName)
        {
            try
            {
                var userType = await _dbContext.UserTypes
                    .Where(ut => ut.TypeName == userTypeName)
                    .FirstOrDefaultAsync();

                return userType;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<UserType> GetUserTypeByIdAsync(Guid id)
        {
            try
            {
                var userType = await _dbContext.UserTypes
                     .FirstOrDefaultAsync(ut => ut.Id == id);

                return userType;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<Guid> AddUserTypeAsync(UserType userType)
        {
            try
            {
                await _dbContext.UserTypes.AddAsync(userType);
                await _dbContext.SaveChangesAsync();

                return userType.Id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<bool> UpdateUserTypeAsync(Guid userTypeId, UserTypeRequest userTypeDto)
        {
            try
            {
                var existingUserType = await _dbContext.UserTypes.FindAsync(userTypeId);

                if (existingUserType == null)
                    return false;

                existingUserType.TypeName = userTypeDto.TypeName;

                _dbContext.UserTypes.Update(existingUserType);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<bool> DeleteUserTypeAsync(Guid userTypeId)
        {
            try
            {
                var userType = await _dbContext.UserTypes.FindAsync(userTypeId);

                if (userType == null)
                    return false;

                _dbContext.UserTypes.Remove(userType);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }
    }
}
