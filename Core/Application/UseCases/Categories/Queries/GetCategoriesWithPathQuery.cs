using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Application.Interfaces;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Queries;

public class GetCategoriesWithPathQuery : IRequest<IEnumerable<CategoryWithPathDTO>>
{
    #region Handler
    public class GetCategoriesWithPathQueryHandler : IRequestHandler<GetCategoriesWithPathQuery, IEnumerable<CategoryWithPathDTO>>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        private readonly ICategoryDapperRepository categoryDapperRepository;
        #endregion

        #region Constructor
        public GetCategoriesWithPathQueryHandler(
            IMapper mapper, 
            ICategoryService categoryService, 
            ICategoryDapperRepository categoryDapperRepository            
        )
        {
            this.mapper = mapper;
            this.categoryService = categoryService;
            this.categoryDapperRepository = categoryDapperRepository;            
        }
        #endregion

        #region Handle
        public async Task<IEnumerable<CategoryWithPathDTO>> Handle(GetCategoriesWithPathQuery request, CancellationToken cancellationToken)
        {
            var categories = await categoryDapperRepository.GetAll();
            var categoriesWithPath = mapper.Map<IEnumerable<CategoryWithPathDTO>>(categories);

            foreach (var item in categoriesWithPath)
            {
                item.Path = await categoryService.GetPath(item.ID);
            }

            return categoriesWithPath.OrderBy(c => c.Name);
        }
        #endregion
    }
    #endregion
}