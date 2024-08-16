using System.ComponentModel.DataAnnotations;
using TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;

namespace TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdNumber { get; set; }
        public string Location { get; set; }
        public string DateOfBirth { get; set; }
        public string Password { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public bool Is2FAEnabled { get; set; } = false;
        public bool IsAccountVerified { get; set; } = false;
        public bool IsInitialPasswordChanged { get; set; } = false;
        public bool IsPasswordExpired { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public string? Token { get; set; }
        public string? ResetToken { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public string UserType { get; set; }
        public string? LicenseNumber { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }




        // Update user information
        public void UpdateUserInfo(string firstName, string lastName, string email, string phoneNumber, string location, string dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Location = location;
            DateOfBirth = dateOfBirth;
            UpdatedDate = DateTime.UtcNow;
        }

        public void SetResetToken(string token)
        {
            ResetToken = BCrypt.Net.BCrypt.HashPassword(token);
            UpdatedDate = DateTime.UtcNow;
        }

        public void ClearResetToken(User user)
        {
            user.ResetToken = null;
        }

        // Confirm email
        public void ConfirmEmail()
        {
            IsEmailConfirmed = true;
            UpdatedDate = DateTime.UtcNow;
        }

        // Enable or disable 2FA
        public void SetTwoFactorAuthentication(bool isEnabled)
        {
            Is2FAEnabled = isEnabled;
            UpdatedDate = DateTime.UtcNow;
        }

        // Verify account
        public void VerifyAccount()
        {
            IsAccountVerified = true;
            UpdatedDate = DateTime.UtcNow;
        }

        // Change password
        public void ChangePassword(string newPassword)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            IsInitialPasswordChanged = true;
            UpdatedDate = DateTime.UtcNow;
        }

        // Set password expiration
        public void ExpirePassword()
        {
            IsPasswordExpired = true;
            UpdatedDate = DateTime.UtcNow;
        }

        // Mark as deleted
        public void DeleteUser()
        {
            IsDeleted = true;
            UpdatedDate = DateTime.UtcNow;
        }

        // Assign a role to the user
        public void AssignRole(UserRole role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            UserRoles.Add(role);
            UpdatedDate = DateTime.UtcNow;
        }

        // Remove a role from the user
        public void RemoveRole(UserRole role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            UserRoles.Remove(role);
            UpdatedDate = DateTime.UtcNow;
        }


    }


}
