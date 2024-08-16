using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities;

namespace TeleAfiaAppDotNet.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }

        // Method to generate a secure 6-digit token
        public string GenerateRandomToken(User user)
        {
            try
            {
                using (var rng = new RNGCryptoServiceProvider())
                {
                    byte[] randomNumber = new byte[4];
                    rng.GetBytes(randomNumber);

                    // Convert the bytes to a uint and ensure it's within the 6-digit range
                    uint generatedValue = BitConverter.ToUInt32(randomNumber, 0);
                    int sixDigitToken = (int)(generatedValue % 900000) + 100000; // Ensures the token is in the range 100000-999999

                    return sixDigitToken.ToString("D6");
                }
            }
            catch (CryptographicException ex)
            {
                throw new ApplicationException("An error occurred while generating a secure token.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred during token generation.", ex);
            }
        }

        // Method to generate a JWT token
        public string GenerateToken(User user)
        {
            try
            {
                // Define signing credentials using the secret key
                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                    SecurityAlgorithms.HmacSha256
                );

                // Create the claims based on user information
                var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new(JwtRegisteredClaimNames.GivenName, user.FirstName),
                    new(JwtRegisteredClaimNames.FamilyName, user.LastName),
                    new(JwtRegisteredClaimNames.Email, user.Email),
                    new(JwtRegisteredClaimNames.Typ, user.UserType),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                if (user.UserRoles != null)
                {
                    foreach (var userRole in user.UserRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole.Role.RoleName));
                    }
                }

                var securityToken = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                    claims: claims,
                    signingCredentials: signingCredentials
                );

                return new JwtSecurityTokenHandler().WriteToken(securityToken);
            }
            catch (SecurityTokenException ex)
            {
                throw new ApplicationException("An error occurred while generating the JWT token.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred during JWT token generation.", ex);
            }
        }
    }
}
