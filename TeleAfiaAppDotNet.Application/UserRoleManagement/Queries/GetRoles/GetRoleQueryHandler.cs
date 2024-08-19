using MediatR;
using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;

namespace TeleAfiaAppDotNet.Application.UserRoleManagement.Queries.GetRoles
{
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, List<Role>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<List<Role>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var roles = _roleRepository.GetAllRoles();
                if (roles == null)
                {
                    throw new Exception("The list of roles is empty");
                }

                return roles;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }
    }
}
