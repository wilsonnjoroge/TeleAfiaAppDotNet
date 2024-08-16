using Microsoft.EntityFrameworkCore;
using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities;
using TeleAfiaAppDotNet.Infrastructure.Data;

namespace TeleAfiaAppDotNet.Infrastructure.Repositories
{
    public class PractitionerRepository : IPractitionerRepository
    {
        private readonly ApplicationDbContext _context;

        public PractitionerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Practitioner> GetPractitionerByIdAsync(Guid id)
        {
            try
            {
                return await _context.Practitioners
                    .Include(p => p.PractitionerTypes)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task UpdatePractitionerAsync(Practitioner practitioner)
        {
            try
            {
                _context.Practitioners.Update(practitioner);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }
    }
}
