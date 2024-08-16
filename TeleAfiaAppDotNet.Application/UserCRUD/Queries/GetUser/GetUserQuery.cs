using MediatR;
using TeleAfiaAppDotNet.Contracts.UserCrudDTOs.GetUserDTOs;

namespace TeleAfiaAppDotNet.Application.UserCRUD.Queries.GetUser
{
    public class GetUserQuery : IRequest<GetUserResponse>
    {
        public GetUserRequest GetUserRequest { get; set; }

        public GetUserQuery(GetUserRequest getUserRequest = null)
        {
            GetUserRequest = getUserRequest;
        }
    }
}
