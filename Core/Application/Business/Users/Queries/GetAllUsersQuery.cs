using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Users.Queries;

public class GetAllUsersQuery : IRequest<IEnumerable<UserDTO>>
{
    #region Handler
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDTO>>
    {
        #region Properties
        private readonly IUserDapperRepository userDapperRepository;
        private readonly IMapper mapper;
        #endregion

        #region Cosntructor
        public GetAllUsersQueryHandler(IUserDapperRepository userDapperRepository, IMapper mapper)
        {
            this.userDapperRepository = userDapperRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<IEnumerable<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await userDapperRepository.GetAll();
            var dto = mapper.Map<IEnumerable<UserDTO>>(users);
            return dto;
        }
        #endregion
    }
    #endregion
}