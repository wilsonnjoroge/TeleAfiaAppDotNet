using MediatR;
using TeleAfiaAppDotNet.Contracts.UserCrudDTOs.UpdateUserDTOs;

namespace TeleAfiaAppDotNet.Application.UserCRUD.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UpdateUserResponse>
    {
        public string? IdNumber { get; set; }
        public string? Email { get; set; }
        public UpdateUserRequest? UpdateUserRequest { get; set; }

        public UpdateUserCommand(UpdateUserRequest updateUserRequest)
        {
            UpdateUserRequest = updateUserRequest;
        }
    }
}
