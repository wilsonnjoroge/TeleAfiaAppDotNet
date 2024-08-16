using MediatR;
using TeleAfiaAppDotNet.Application.Interfaces.UserRoleAndTypeRepositories;
using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.UserTypeDTOs;
using TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;

namespace TeleAfiaAppDotNet.Application.UserTypesManagement.Commands.AddUserType
{
    public class AddUserTypeCommandHandler : IRequestHandler<AddUserTypeCommand, UserTypeResponse>
    {
        private readonly IUserTypeRepository _userTypeRepository;
        public AddUserTypeCommandHandler(IUserTypeRepository userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
        }

        public async Task<UserTypeResponse> Handle(AddUserTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Request = request.UserTypeRequest;

                var userType = await _userTypeRepository.GetUserTypeByNameAsync(Request.TypeName);
                if (userType != null)
                {
                    throw new Exception($"User type {Request.TypeName} already exist");
                }

                var typeToAdd = new UserType
                {
                    TypeName = Request.TypeName,
                };

                await _userTypeRepository.AddUserTypeAsync(typeToAdd);

                var addedType = await _userTypeRepository.GetUserTypeByNameAsync(typeToAdd.TypeName);

                var response = new UserTypeResponse
                {
                    Message = "User type added successfully",
                    TypeId = addedType.Id,
                    TypeName = Request.TypeName
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occoured while executing add user type command handler", ex);

            }
        }
    }
}
