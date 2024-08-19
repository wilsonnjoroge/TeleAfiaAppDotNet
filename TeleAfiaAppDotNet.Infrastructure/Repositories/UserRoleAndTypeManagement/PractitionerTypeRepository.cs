
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.PractitionerTypeDTOs;
using TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;
using TeleAfiaAppDotNet.Infrastructure.Data;

namespace TeleAfiaAppDotNet.Infrastructure.Repositories.UserRoleAndTypeManagement
{
    public class PractitionerTypeRepository : IPractitionerTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PractitionerTypeRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<PractitionerTypeRequest>> GetAllPractitionerTypesAsync()
        {
            try
            {
                var practitionerTypes = await _dbContext.PractitionerTypes
                    .Select(pt => _mapper.Map<PractitionerTypeRequest>(pt))
                    .ToListAsync();

                return practitionerTypes;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<PractitionerTypeRequest> GetPractitionerTypeByIdAsync(Guid practitionerTypeId)
        {
            try
            {
                var practitionerType = await _dbContext.PractitionerTypes
                    .Where(pt => pt.Id == practitionerTypeId)
                    .FirstOrDefaultAsync();

                return _mapper.Map<PractitionerTypeRequest>(practitionerType);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<Guid> AddPractitionerTypeAsync(PractitionerTypeRequest practitionerTypeDto)
        {
            try
            {
                var practitionerType = _mapper.Map<PractitionerType>(practitionerTypeDto);

                _dbContext.PractitionerTypes.Add(practitionerType);
                await _dbContext.SaveChangesAsync();

                return practitionerType.Id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<bool> UpdatePractitionerTypeAsync(Guid practitionerTypeId, PractitionerTypeRequest practitionerTypeDto)
        {
            try
            {
                var existingPractitionerType = await _dbContext.PractitionerTypes.FindAsync(practitionerTypeId);

                if (existingPractitionerType == null)
                    return false;

                existingPractitionerType.TypeName = practitionerTypeDto.TypeName;

                _dbContext.PractitionerTypes.Update(existingPractitionerType);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<bool> DeletePractitionerTypeAsync(Guid practitionerTypeId)
        {
            try
            {
                var practitionerType = await _dbContext.PractitionerTypes.FindAsync(practitionerTypeId);

                if (practitionerType == null)
                    return false;

                _dbContext.PractitionerTypes.Remove(practitionerType);
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
