using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Application.Interfaces;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Articles.Queries;

public class GetArticleByIdQuery : IRequest<ArticleDTO>
{
    #region Query Properties
    public int ID { get; set; }
    #endregion

    #region Handler
    public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ArticleDTO>
    {
        #region Properties
        private readonly IArticleRepository articleRepository;
        private readonly IUserRepository userRepository;
        private readonly IHtmlSanitizer htmlSanitizer;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetArticleByIdQueryHandler(
            IArticleRepository articleRepository, 
            IMapper mapper, 
            IUserRepository userRepository,
            IHtmlSanitizer htmlSanitizer
        )
        {
            this.articleRepository = articleRepository;            
            this.userRepository = userRepository;
            this.htmlSanitizer = htmlSanitizer;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<ArticleDTO> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var article = await articleRepository.Get(request.ID);
            var user = await userRepository.Get(article.UserId);
            var dto = mapper.Map<ArticleDTO>(article);

            dto.Author = user.Name;
            
            return dto;
        }
        #endregion
    }
    #endregion
}