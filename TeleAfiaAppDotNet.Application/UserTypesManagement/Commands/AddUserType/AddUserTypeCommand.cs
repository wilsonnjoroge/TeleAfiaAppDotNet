using MediatR;
using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.UserTypeDTOs;

namespace TeleAfiaAppDotNet.Application.UserTypesManagement.Commands.AddUserType
{
    public class AddUserTypeCommand : IRequest<UserTypeResponse>
    {
        public UserTypeRequest UserTypeRequest { get; set; }
        public AddUserTypeCommand(UserTypeRequest userTypeRequest)
        {
            UserTypeRequest = userTypeRequest;
        }
    }
}
