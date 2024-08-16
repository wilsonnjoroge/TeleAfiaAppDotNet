
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities;
using TeleAfiaAppDotNet.Infrastructure.Data;

namespace TeleAfiaAppDotNet.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<User> GetUserByEmailOrIdNumberAsync(string email, string idNumber)
        {
            try
            {
                return await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == email || u.IdNumber == idNumber);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<User> GetUserByIdAsync(string idNumber)
        {
            try
            {
                return await _context.Users
                    .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => u.IdNumber == idNumber);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _context.Users
                    .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => u.Email == email);

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task DeleteUserAsync(string idNumber, string email)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == email || u.IdNumber == idNumber);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<bool> UserExists(string email)
        {
            var user = await _context.Users
                     .FirstOrDefaultAsync(u => u.Email == email);
            if (user is not null)
            {
                return true;
            }

            return false;
        }
    }
}
