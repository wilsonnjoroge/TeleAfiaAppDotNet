using TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities;

namespace TeleAfiaAppDotNet.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);

        string GenerateRandomToken(User user);
    }
}

