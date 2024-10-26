using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Queries;

public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDTO>>
{
    #region Handler
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDTO>>
    {
        #region Properties        
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetAllCategoriesQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<IEnumerable<CategoryDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {            
            var categories = await categoryRepository.GetAll();
            var dto = mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return dto;
        }
        #endregion
    }
    #endregion
}