using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeleAfiaAppDotNet.Application.UserRoleManagement.Commands.AddRole;
using TeleAfiaAppDotNet.Application.UserRoleManagement.Commands.DeleteRole;
using TeleAfiaAppDotNet.Application.UserRoleManagement.Commands.UpdateRole;
using TeleAfiaAppDotNet.Application.UserRoleManagement.Queries.GetRoles;
using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.UserRoleDTOs;

namespace TeleAfiaAppDotNet.Api.Controllers.UserRoleManagement
{
    [ApiController]
    [Route("User-Roles")]
    public class UserRoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserRoleController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet("view-all-roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var query = new GetRoleQuery();
                var roles = await _mediator.Send(query);

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occoured while executing view all user roles controller: {ex}");
            }
        }


        [HttpPost("create-roles")]
        public async Task<IActionResult> AddUserRole([FromBody] UserRoleRequest userRoleRequest)
        {
            try
            {
                var command = new AddRoleCommand(userRoleRequest);

                var response = await _mediator.Send(command);

                var mappedResponse = _mapper.Map<UserRoleResponse>(response);

                return Ok(mappedResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occoured while executing create user role controller: {ex}");
            }
        }

        [HttpPut("update-roles")]
        public async Task<IActionResult> UpdateRoles([FromQuery] Guid roleId, [FromBody] UserRoleRequest userRoleRequest)
        {
            try
            {
                var command = new UpdateRoleCommand(roleId, userRoleRequest.RoleName);

                var response = await _mediator.Send(command);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occoured while executing update user role controller: {ex}");
            }
        }

        [HttpDelete("delete-role")]
        public async Task<IActionResult> DeleteRole([FromQuery] Guid roleId)
        {
            try
            {
                var command = new DeleteRoleCommand(roleId);

                var response = await _mediator.Send(command);
                var mappedResponse = new UserRoleResponse
                {
                    Message = response.Message
                };

                return Ok(mappedResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occoured while executing delete user role controller: {ex}");
            }
        }
    }
}
