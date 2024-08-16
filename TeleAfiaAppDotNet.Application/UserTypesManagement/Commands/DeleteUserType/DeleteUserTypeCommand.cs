using MediatR;
using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.UserTypeDTOs;

namespace TeleAfiaAppDotNet.Application.UserTypesManagement.Commands.DeleteUserType
{
    public class DeleteUserTypeCommand : IRequest<UserTypeResponse>
    {
        public Guid TypeId { get; set; }
        public DeleteUserTypeCommand(Guid typeId)
        {
            TypeId = typeId;
        }
    }
}
