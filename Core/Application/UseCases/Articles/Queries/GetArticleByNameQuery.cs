using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Articles.Queries;

public class GetArticleByNameQuery : IRequest<ArticleDTO>
{
    #region Query Properties
    public string Name { get; set; }
    #endregion

    #region Handler
    public class GetArticleByNameQueryHandler : IRequestHandler<GetArticleByNameQuery, ArticleDTO>
    {
        #region Properties
        private readonly IArticleDapperRepository articleDapperRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetArticleByNameQueryHandler(IArticleDapperRepository articleDapperRepository, IMapper mapper)
        {
            this.articleDapperRepository = articleDapperRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<ArticleDTO> Handle(GetArticleByNameQuery request, CancellationToken cancellationToken)
        {
            var article = await articleDapperRepository.GetByName(request.Name);
            var dto = mapper.Map<ArticleDTO>(article);
            return dto;
        }
        #endregion
    }
    #endregion
}