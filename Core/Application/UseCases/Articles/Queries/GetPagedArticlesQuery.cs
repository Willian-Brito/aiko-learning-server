using AikoLearning.Core.Application.Base;
using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Articles.Queries;

public class GetPagedArticlesQuery : IRequest<PagedResult<ArticleDTO>>
{
    #region Query Properties
    public int PageNumber { get; set; }
    public int PageLimit { get; set; }
    #endregion

    #region Constructor
    public GetPagedArticlesQuery(int pageNumber, int pageLimit)
    {
        PageNumber = pageNumber;
        PageLimit = pageLimit;
    }
    #endregion

    #region Handler
    public class GetPagedArticlesQueryHandler  
        : IRequestHandler<GetPagedArticlesQuery, PagedResult<ArticleDTO>>
    {
        #region Properties
        private readonly IArticleDapperRepository articleDapperRepository;
        private readonly IArticleRepository articleRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetPagedArticlesQueryHandler(
            IArticleDapperRepository articleDapperRepository,
            IArticleRepository articleRepository,
            IMapper mapper
        )
        {
            this.articleDapperRepository = articleDapperRepository;
            this.articleRepository = articleRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<PagedResult<ArticleDTO>> Handle(
            GetPagedArticlesQuery request, 
            CancellationToken cancellationToken
        )
        {
            var totalCount = await articleRepository.Count();
            var items = await articleRepository.GetPaged(request.PageNumber, request.PageLimit);
            var articles = mapper.Map<IEnumerable<ArticleDTO>>(items);

            return new PagedResult<ArticleDTO>
            (
                items: articles,
                pageNumber: request.PageNumber,
                pageLimit: request.PageLimit,
                totalCount: totalCount
            );
        }
        #endregion
    }
    #endregion
}