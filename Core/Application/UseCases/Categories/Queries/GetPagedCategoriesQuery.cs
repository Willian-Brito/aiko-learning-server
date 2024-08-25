using AikoLearning.Core.Application.Base;
using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Queries;

public class GetPagedCategoriesQuery : IRequest<PagedResult<CategoryDTO>>
{
    #region Query Properties
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    #endregion

    #region Constructor
    public GetPagedCategoriesQuery(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
    #endregion

    #region Handler
    public class GetPagedCategoriesQueryHandler  
        : IRequestHandler<GetPagedCategoriesQuery, PagedResult<CategoryDTO>>
    {
        #region Properties
        private readonly ICategoryDapperRepository categoryDapperRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetPagedCategoriesQueryHandler(
            ICategoryDapperRepository categoryDapperRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper
        )
        {
            this.categoryDapperRepository = categoryDapperRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<PagedResult<CategoryDTO>> Handle(
            GetPagedCategoriesQuery request, 
            CancellationToken cancellationToken
        )
        {
            var totalCount = await categoryRepository.Count();
            var items = await categoryRepository.GetPaged(request.PageIndex, request.PageSize);
            var categories = mapper.Map<IEnumerable<CategoryDTO>>(items);

            return new PagedResult<CategoryDTO>
            (
                items: categories,
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                totalCount: totalCount
            );
        }
        #endregion
    }
    #endregion
}