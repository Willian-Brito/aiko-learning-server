using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Queries;

public class GetCategoryByNameQuery : IRequest<CategoryDTO>
{
    #region Query Properties
    public string Name { get; set; }
    #endregion

    #region Handler
    public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, CategoryDTO>
    {
        #region Properties
        private readonly ICategoryDapperRepository categoryDapperRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetCategoryByNameQueryHandler(ICategoryDapperRepository categoryDapperRepository, IMapper mapper)
        {
            this.categoryDapperRepository = categoryDapperRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<CategoryDTO> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var category = await categoryDapperRepository.GetByName(request.Name);
            var dto = mapper.Map<CategoryDTO>(category);
            return dto;
        }
        #endregion
    }
    #endregion
}