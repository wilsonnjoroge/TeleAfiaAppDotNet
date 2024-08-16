using System.ComponentModel.DataAnnotations;

namespace TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.ResetPasswordDTOs
{
    public class ResetPasswordRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please provide a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Reset code is required")]
        public string ResetCode { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }
    }
}
