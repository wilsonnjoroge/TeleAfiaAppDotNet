namespace TeleAfiaAppDotNet.Contracts.UserCrudDTOs.GetUserDTOs
{
    public class GetUserResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdNumber { get; set; }
        public string Location { get; set; }
        public string DateOfBirth { get; set; }
        public string UserType { get; set; }
        public string? LicenseNumber { get; set; }
    }
}
