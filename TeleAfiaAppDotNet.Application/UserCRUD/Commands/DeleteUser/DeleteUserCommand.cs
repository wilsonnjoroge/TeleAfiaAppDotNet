using MediatR;
using TeleAfiaAppDotNet.Contracts.UserCrudDTOs.DeleteUserDTOs;

namespace TeleAfiaAppDotNet.Application.UserCRUD.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        public string IdNumber { get; set; }
        public string Email { get; set; }

        public DeleteUserCommand()
        {
        }
    }
}
