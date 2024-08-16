using System.ComponentModel.DataAnnotations;

namespace TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.ForgotPasswordDTOs
{
    public class ForgotPasswordRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please input a valid email address")]
        public string Email { get; set; }
    }
}
