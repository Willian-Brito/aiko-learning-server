using AikoLearning.Core.Domain.Account;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Users.Queries;

public class GetAllRolesQuery : IRequest<List<string>>
{
    #region Handler
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<string>>
    {
        #region Properties                
        private readonly IRoleService roleService;        
        #endregion

        #region Constructor
        public GetAllRolesQueryHandler(IRoleService roleService)
        {            
            this.roleService = roleService;            
        }
        #endregion

        #region Handle
        public async Task<List<string>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {            
            return roleService.GetAllRolesNames();
        }
        #endregion
    }
    #endregion
}