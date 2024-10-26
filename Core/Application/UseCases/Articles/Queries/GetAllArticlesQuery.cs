using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Articles.Queries;

public class GetAllArticlesQuery : IRequest<IEnumerable<ArticleDTO>>
{
    #region Handler
    public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, IEnumerable<ArticleDTO>>
    {
        #region Properties
        private readonly IArticleRepository articleRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetAllArticlesQueryHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            this.articleRepository = articleRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<IEnumerable<ArticleDTO>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = await articleRepository.GetAll();
            var dto = mapper.Map<IEnumerable<ArticleDTO>>(articles);
            return dto;
        }
        #endregion
    }
    #endregion
}