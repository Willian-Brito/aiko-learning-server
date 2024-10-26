using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Queries;

public class GetCategoriesWithTreeQuery : IRequest<IEnumerable<CategoryDTO>>
{
    #region Handler
    public class GetCategoriesWithTreeHandler : IRequestHandler<GetCategoriesWithTreeQuery, IEnumerable<CategoryDTO>>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;        
        #endregion

        #region Constructor
        public GetCategoriesWithTreeHandler(
            IMapper mapper, 
            ICategoryService categoryService                   
        )
        {
            this.mapper = mapper;
            this.categoryService = categoryService;                     
        }
        #endregion

        #region Handle
        public async Task<IEnumerable<CategoryDTO>> Handle(
            GetCategoriesWithTreeQuery request, 
            CancellationToken cancellationToken
        )
        {
            var models = await categoryService.GetAll();
            var parents = models.Where(m => m.ParentId == null);
            var categories = mapper.Map<IEnumerable<CategoryDTO>>(parents);

            foreach (var category in categories)
            {
                var children = await categoryService.GetTree((int)category.ID);
                category.Children = children;
            }

            return categories;
        }
        #endregion
    }
    #endregion
}