using System.ComponentModel.DataAnnotations;

namespace TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.RegisterUserDTOs
{
    public class UserRegistrationDto
    {

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please input a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        [Phone(ErrorMessage = "Please input a valid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Id Number is required")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Location is Required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "DateofBirth is required")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string UserType { get; set; }
        public string? LicenseNumber { get; set; }

        public List<string> UserRoles { get; set; }


    }
}