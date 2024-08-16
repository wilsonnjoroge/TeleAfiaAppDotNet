using AutoMapper;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeleAfiaAppDotNet.Application.UserCRUD.Commands.DeleteUser;
using TeleAfiaAppDotNet.Application.UserCRUD.Commands.UpdateUser;
using TeleAfiaAppDotNet.Application.UserCRUD.Queries.GetUser;
using TeleAfiaAppDotNet.Contracts.UserCrudDTOs.DeleteUserDTOs;
using TeleAfiaAppDotNet.Contracts.UserCrudDTOs.GetUserDTOs;
using TeleAfiaAppDotNet.Contracts.UserCrudDTOs.UpdateUserDTOs;

namespace TeleAfiaAppDotNet.Api.Controllers.UserCRUD
{
    [ApiController]
    [Route("Users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get-one-user")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] GetUserRequest getUserRequest = null)
        {
            try
            {
                var query = new GetUserQuery(getUserRequest);

                var response = await _mediator.Send(query);

                if (response == null)
                {
                    return NotFound("User not found");
                }

                var mappedResponse = _mapper.Map<GetUserResponse>(response);

                return Ok(mappedResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occoured while executing get one user controller: {ex}");
            }
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var query = new GetAllUsersQuery();
                var users = await _mediator.Send(query);

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occoured while executing get all users controller: {ex}");
            }
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUserDetails(string? idNumber, string? email, [FromBody] UpdateUserRequest updateUserRequest)
        {
            try
            {
                var command = new UpdateUserCommand(updateUserRequest)
                {
                    IdNumber = idNumber,
                    Email = email
                };

                var response = await _mediator.Send(command);

                var mappedResponse = _mapper.Map<UpdateUserResponse>(response);
                return Ok(mappedResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred while executing update user controller: {ex}");
            }
        }


        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(string? idNumber, string? email)
        {
            try
            {
                var command = new DeleteUserCommand()
                {
                    IdNumber = idNumber,
                    Email = email
                }; ;
                var response = await _mediator.Send(command);

                var mappedResponse = _mapper.Map<DeleteUserResponse>(response);
                return Ok(mappedResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occoured while executing delete user controller: {ex}");
            }
        }
    }
}
