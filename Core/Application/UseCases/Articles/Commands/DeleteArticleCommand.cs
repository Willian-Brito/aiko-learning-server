using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Exceptions;
using AikoLearning.Core.Domain.Interfaces;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Commands;

public sealed class DeleteArticleCommand : IRequest<Article>
{
    #region Command Properties
    public int ID { get; set; }
    #endregion

    #region Handler
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Article>
    {
        #region Properties
        private readonly IUnitOfWork unityOfWork;
        private readonly IArticleRepository articleRepository;
        #endregion

        #region Constructor
        public DeleteArticleCommandHandler(IUnitOfWork unityOfWork, IArticleRepository articleRepository)
        {
            this.unityOfWork = unityOfWork;        
            this.articleRepository = articleRepository;
        }
        #endregion

        #region Handle

        public async Task<Article> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await articleRepository.Delete(request.ID);

            if (article is null) throw new NotFoundException("Artigo não existe!");                
                            
            await unityOfWork.Commit();
            return article;
        }
        #endregion
    }
    #endregion
}