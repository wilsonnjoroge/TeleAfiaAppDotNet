using TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities;

namespace TeleAfiaAppDotNet.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByEmailOrIdNumberAsync(string? idNumber, string? email);
        Task<User> GetUserByIdAsync(string idNumber);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UserExists(string email);
        Task AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(string idNumber, string email);
    }
}
