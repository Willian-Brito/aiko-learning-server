using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Application.Interfaces;
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
        private readonly IHtmlSanitizer htmlSanitizer;
        private readonly IArticleRepository articleRepository;
        #endregion

        #region Constructor
        public CreateArticleCommandHandler(
            IMapper mapper,
            IUnitOfWork unityOfWork, 
            IHtmlSanitizer htmlSanitizer,
            IArticleRepository articleRepository
        )
        {
            this.mapper = mapper;
            this.unityOfWork = unityOfWork;
            this.htmlSanitizer = htmlSanitizer;
            this.articleRepository = articleRepository;
        }
        #endregion

        #region Handle

        public async Task<ArticleDTO> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {         
            var sanitizedBytes = htmlSanitizer.Sanitize(request.Content);

            var newArticle = new Article
            (
                request.Name, 
                request.CategoryId ?? 0, 
                request.UserId ?? 0, 
                request.Description, 
                sanitizedBytes,                 
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