using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Commands;

public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDTO>>
{
    #region Handler
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDTO>>
    {
        #region Properties
        private readonly ICategoryDapperRepository categoryDapperRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetAllCategoriesQueryHandler(ICategoryDapperRepository categoryDapperRepository, IMapper mapper)
        {
            this.categoryDapperRepository = categoryDapperRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<IEnumerable<CategoryDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await categoryDapperRepository.GetAll();
            var dto = mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return dto;
        }
        #endregion
    }
    #endregion
}