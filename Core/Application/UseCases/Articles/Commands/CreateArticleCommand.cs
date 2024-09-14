using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Exceptions;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Commands;

public sealed class CreateArticleCommand : ArticleCommand
{
    #region Handler
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleDTO>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly IUnitOfWork unityOfWork;
        private readonly IArticleRepository articleRepository;
        #endregion

        #region Constructor
        public CreateArticleCommandHandler(
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

        public async Task<ArticleDTO> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var newArticle = new Article
            (
                request.Name, 
                request.CategoryId, 
                request.UserId, 
                request.Description, 
                request.Content,                 
                request.ImageUrl
            );

            if (newArticle is null) throw new BadRequestException("Erro ao criar Artigo!");
                            
            var model = await articleRepository.Insert(newArticle);
            var dto = mapper.Map<ArticleDTO>(model);
            await unityOfWork.Commit();

            dto.ID = model.ID;

            return dto;
        }
        #endregion
    }
    #endregion
}