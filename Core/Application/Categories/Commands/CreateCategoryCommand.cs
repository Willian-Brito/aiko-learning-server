using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Commands;

public sealed class CreateCategoryCommand : CommandCategory
{
    #region Handler
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDTO>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly IUnitOfWork unityOfWork;
        private readonly ICategoryRepository categoryRepository;
        #endregion

        #region Constructor
        public CreateCategoryCommandHandler(
            IMapper mapper,
            IUnitOfWork unityOfWork, 
            ICategoryRepository categoryRepository
        )
        {
            this.mapper = mapper;
            this.unityOfWork = unityOfWork;        
            this.categoryRepository = categoryRepository;
        }
        #endregion

        #region Handle

        public async Task<CategoryDTO> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Category(request.Name, request.ParentId);

            if (newCategory == null)
                throw new ApplicationException("Erro ao criar categoria!");
                            
            await categoryRepository.Insert(newCategory);
            await unityOfWork.Commit();
            
            var dto = mapper.Map<CategoryDTO>(newCategory);
            return dto;
        }
        #endregion
    }
    #endregion
}