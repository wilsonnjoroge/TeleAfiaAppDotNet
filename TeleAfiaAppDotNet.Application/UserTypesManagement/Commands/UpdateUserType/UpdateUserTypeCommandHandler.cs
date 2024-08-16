using MediatR;
using TeleAfiaAppDotNet.Application.Interfaces.UserRoleAndTypeRepositories;
using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.UserTypeDTOs;

namespace TeleAfiaAppDotNet.Application.UserTypesManagement.Commands.UpdateUserType
{
    public class UpdateUserTypeCommandHandler : IRequestHandler<UpdateUserTypeCommand, UserTypeResponse>
    {
        private readonly IUserTypeRepository _userTypeRepository;
        public UpdateUserTypeCommandHandler(IUserTypeRepository userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
        }
        public async Task<UserTypeResponse> Handle(UpdateUserTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Request = request.TypeRequest;

                var userType = await _userTypeRepository.GetUserTypeByIdAsync(request.TypeId);
                if (userType == null)
                {
                    throw new Exception($"User type with ID '{request.TypeId}' does not exist");
                }

                var typeToUpdate = new UserTypeRequest
                {
                    TypeName = Request.TypeName
                };

                await _userTypeRepository.UpdateUserTypeAsync(request.TypeId, typeToUpdate);

                var response = new UserTypeResponse
                {
                    Message = "User type updated successfully",
                    TypeId = request.TypeId,
                    TypeName = typeToUpdate.TypeName
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occoured while executing update user type command handler", ex);

            }
        }
    }
}
