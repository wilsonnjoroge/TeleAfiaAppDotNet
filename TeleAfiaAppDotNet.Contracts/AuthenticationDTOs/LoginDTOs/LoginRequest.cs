using System.ComponentModel.DataAnnotations;

namespace TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.LoginDTOs
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please input a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
