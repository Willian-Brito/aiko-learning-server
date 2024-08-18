using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Commands;

public sealed class UpdateArticleCommand : ArticleCommand
{
    #region Command Properties
    public int ID { get; set; }
    #endregion

    #region Handler
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, ArticleDTO>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly IUnitOfWork unityOfWork;
        private readonly IArticleRepository articleRepository;
        #endregion

        #region Constructor
        public UpdateArticleCommandHandler(
            IMapper mapper,
            IUnitOfWork unityOfWork, 
            IArticleRepository articleRepository
        )
        {
            this.mapper = mapper;
            this.unityOfWork = unityOfWork;        
            this.articleRepository = articleRepository;
        }
        #endregion

        #region Handle

        public async Task<ArticleDTO> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await articleRepository.Get(request.ID);

            if (article == null)
                throw new InvalidOperationException("Artigo não existe!");
                            
            article.Update(
                request.Name, 
                request.CategoryId, 
                request.UserId, 
                request.Description, 
                request.ImageUrl,
                request.Content        
            );
            
            await articleRepository.Update(article);
            var dto = mapper.Map<ArticleDTO>(article);

            await unityOfWork.Commit();
            return dto;
        }
        #endregion
    }
    #endregion
}