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
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        #endregion

        #region Cosntructor
        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<IEnumerable<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepository.GetAll();
            var dto = mapper.Map<IEnumerable<UserDTO>>(users);
            return dto;
        }
        #endregion
    }
    #endregion
}