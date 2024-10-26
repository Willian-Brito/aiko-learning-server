using AikoLearning.Core.Application.Base;
using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Queries;

public class GetPagedCategoriesQuery : IRequest<PagedResult<CategoryDTO>>
{
    #region Query Properties
    public int PageNumber { get; set; }
    public int PageLimit { get; set; }
    #endregion

    #region Constructor
    public GetPagedCategoriesQuery(int pageNumber, int pageLimit)
    {
        PageNumber = pageNumber;
        PageLimit = pageLimit;
    }
    #endregion

    #region Handler
    public class GetPagedCategoriesQueryHandler  
        : IRequestHandler<GetPagedCategoriesQuery, PagedResult<CategoryDTO>>
    {
        #region Properties        
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetPagedCategoriesQueryHandler(            
            ICategoryRepository categoryRepository,
            IMapper mapper
        )
        {            
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
            var items = await categoryRepository.GetPaged(request.PageNumber, request.PageLimit);
            var categories = mapper.Map<IEnumerable<CategoryDTO>>(items);

            return new PagedResult<CategoryDTO>
            (
                items: categories,
                pageNumber: request.PageNumber,
                pageLimit: request.PageLimit,
                totalCount: totalCount
            );
        }
        #endregion
    }
    #endregion
}