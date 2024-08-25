using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Articles.Queries;

public class GetArticleByIdQuery : IRequest<ArticleDTO>
{
    #region Query Properties
    public int ID { get; set; }
    #endregion

    #region Handler
    public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ArticleDTO>
    {
        #region Properties
        private readonly IArticleDapperRepository articleDapperRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetArticleByIdQueryHandler(IArticleDapperRepository articleDapperRepository, IMapper mapper)
        {
            this.articleDapperRepository = articleDapperRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<ArticleDTO> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var article = await articleDapperRepository.GetById(request.ID);
            var dto = mapper.Map<ArticleDTO>(article);
            return dto;
        }
        #endregion
    }
    #endregion
}