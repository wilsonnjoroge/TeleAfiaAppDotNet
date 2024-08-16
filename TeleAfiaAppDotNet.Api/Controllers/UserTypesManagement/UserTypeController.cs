using AutoMapper;
using EquityAfia.UserManagement.Application.UserTypesManagement.Commands.AddUserType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeleAfiaAppDotNet.Application.UserTypesManagement.Commands.DeleteUserType;
using TeleAfiaAppDotNet.Application.UserTypesManagement.Commands.UpdateUserType;
using TeleAfiaAppDotNet.Application.UserTypesManagement.Queries.GetUserTypes;
using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.UserTypeDTOs;

namespace EquityAfia.UserManagement.Api.Controllers.UserTypesManagement
{
    [ApiController]
    [Route("User-Types")]
    public class UserTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public UserTypeController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("view-all-usertypes")]
        public async Task<IActionResult> GetAllUserTypes()
        {
            try
            {
                var query = new GetUserTypeQuery();

                var response = await _mediator.Send(query);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occoured while executing view all user types controller: {ex}");
            }
        }

        [HttpPost("add-usertype")]
        public async Task<IActionResult> AddUserType([FromBody] UserTypeRequest userTypeRequest)
        {
            try
            {
                var command = new AddUserTypeCommand(userTypeRequest);

                var response = await _mediator.Send(command);
                var mappedResponse = _mapper.Map<UserTypeResponse>(response);

                return Ok(mappedResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occoured while executing add user type controller: {ex}");
            }
        }

        [HttpPut("update-type/{typeId}")]
        public async Task<IActionResult> UpdateUserType(Guid typeId, UserTypeRequest typeRequest)
        {
            try
            {
                var command = new UpdateUserTypeCommand(typeRequest, typeId);

                var response = await _mediator.Send(command);
                var mappoedResponse = _mapper.Map<UserTypeResponse>(response);

                return Ok(mappoedResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occoured while executing update user type controller: {ex}");
            }
        }

        [HttpDelete("delete-usertype/{typeId}")]
        public async Task<IActionResult> DeleteUserType (Guid typeId)
        {
            try
            {
                var command = new DeleteUserTypeCommand(typeId);
                var response = await _mediator.Send(command);

                var mappedResponse = _mapper.Map<UserTypeResponse>(response);

                return Ok(mappedResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occoured while executing delete user type controller: {ex}");
            }
        }
    }
}
