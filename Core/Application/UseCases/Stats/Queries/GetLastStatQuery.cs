using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Stats;

public class GetLastStatQuery : IRequest<StatDTO>
{
    #region Handler
    public class GetLastStatHandler : IRequestHandler<GetLastStatQuery, StatDTO>
    {
        #region Properties
        private readonly IStatRepository statRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetLastStatHandler(IStatRepository statRepository, IMapper mapper)
        {
            this.statRepository = statRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<StatDTO> Handle(GetLastStatQuery request, CancellationToken cancellationToken)
        {
            var stat = await statRepository.GetLast();
            var dto = mapper.Map<StatDTO>(stat);
            return dto;
        }
        #endregion
    }
    #endregion
}