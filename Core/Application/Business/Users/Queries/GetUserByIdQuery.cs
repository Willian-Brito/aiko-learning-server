using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Users.Queries;

public class GetUserByIdQuery : IRequest<UserDTO>
{
    #region Query Properties
    public int ID { get; set; }
    #endregion

    #region Handler
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        #region Properties
        private readonly IUserDapperRepository userDapperRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetUserByIdQueryHandler(IUserDapperRepository userDapperRepository, IMapper mapper)
        {
            this.userDapperRepository = userDapperRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userDapperRepository.GetById(request.ID);
            var dto = mapper.Map<UserDTO>(user);
            return dto;
        }
        #endregion
    }
    #endregion
}