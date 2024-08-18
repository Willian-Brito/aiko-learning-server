using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Commands;

public class GetCategoryByIdQuery : IRequest<CategoryDTO>
{
    #region Query Properties
    public int ID { get; set; }
    #endregion

    #region Handler
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDTO>
    {
        #region Properties
        private readonly ICategoryDapperRepository categoryDapperRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetCategoryByIdQueryHandler(ICategoryDapperRepository categoryDapperRepository, IMapper mapper)
        {
            this.categoryDapperRepository = categoryDapperRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<CategoryDTO> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await categoryDapperRepository.GetById(request.ID);
            var dto = mapper.Map<CategoryDTO>(category);
            return dto;
        }
        #endregion
    }
    #endregion
}