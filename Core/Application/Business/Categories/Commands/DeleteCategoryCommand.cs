using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Interfaces;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Commands;

public sealed class DeleteCategoryCommand : IRequest<Category>
{
    #region Command Properties
    public int ID { get; set; }
    #endregion

    #region Handler
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Category>
    {
        #region Properties
        private readonly IUnitOfWork unityOfWork;
        private readonly ICategoryRepository categoryRepository;
        #endregion

        #region Constructor
        public DeleteCategoryCommandHandler(IUnitOfWork unityOfWork, ICategoryRepository categoryRepository)
        {
            this.unityOfWork = unityOfWork;        
            this.categoryRepository = categoryRepository;
        }
        #endregion

        #region Handle

        public async Task<Category> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.Delete(request.ID);

            if (category == null)
                throw new InvalidOperationException("Categoria não existe!");
                            
            await unityOfWork.Commit();
            return category;
        }
        #endregion
    }
    #endregion
}