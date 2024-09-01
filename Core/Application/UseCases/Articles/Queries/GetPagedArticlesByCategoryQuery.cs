using AikoLearning.Core.Application.Base;
using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Articles.Queries;

public class GetPagedArticlesByCategoryQuery : IRequest<PagedResult<ArticleByCategoryDTO>>
{
    #region Query Properties
    public int ID { get; set; }
    public int PageNumber { get; set; }
    public int PageLimit { get; set; }
    #endregion

    #region Constructor
    public GetPagedArticlesByCategoryQuery(int id, int pageNumber, int pageLimit)
    {
        ID = id;
        PageNumber = pageNumber;
        PageLimit = pageLimit;
    }
    #endregion

    #region Handler
    public class GetPagedArticlesByCategoryQueryHandler
        : IRequestHandler<GetPagedArticlesByCategoryQuery, PagedResult<ArticleByCategoryDTO>>
    {
        #region Properties
        private readonly ICategoryDapperRepository categoryDapperRepository;
        private readonly IArticleDapperRepository articleDapperRepository;
        private readonly IArticleRepository articleRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetPagedArticlesByCategoryQueryHandler(
            ICategoryDapperRepository categoryDapperRepository,
            IArticleDapperRepository articleDapperRepository,
            IArticleRepository articleRepository,
            IMapper mapper
        )
        {
            this.categoryDapperRepository = categoryDapperRepository;
            this.articleDapperRepository = articleDapperRepository;
            this.articleRepository = articleRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<PagedResult<ArticleByCategoryDTO>> Handle(
            GetPagedArticlesByCategoryQuery request, 
            CancellationToken cancellationToken
        )
        {
            var pageNumber = request.PageNumber == 0 ? 0 : request.PageNumber - 1;
            var categoryId = request.ID;
            var totalCount = await articleRepository.Count();
            var categoryIDs = await categoryDapperRepository.GetCategoryIDsWithChildren(categoryId);            
            var items = await articleDapperRepository.GetPagedByCategories(categoryIDs, pageNumber, request.PageLimit);            
            var articles = mapper.Map<IEnumerable<ArticleByCategoryDTO>>(items);

            return new PagedResult<ArticleByCategoryDTO>
            (
                items: articles.OrderByDescending(a => a.ID).ToList(),
                pageNumber: request.PageNumber,
                pageLimit: request.PageLimit,
                totalCount: totalCount
            );
        }
        #endregion
    }
    #endregion
}