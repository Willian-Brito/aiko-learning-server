using AikoLearning.Core.Domain.Account;
using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Exceptions;
using AikoLearning.Core.Domain.Interfaces;
using MediatR;

namespace AikoLearning.Core.Application.Users.Commands;

public sealed class DeleteUserCommand : IRequest<User>
{
    #region Command Properties
    public int ID { get; set; }
    #endregion

    #region Handler
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, User>
    {
        #region Properties
        private readonly IUnitOfWork unityOfWork;
        private readonly IUserRepository userRepository;
        private readonly IUserTokenRepository userTokenRepository;
        private readonly IArticleRepository articleRepository;
        private readonly ISessionService sessionService;
        #endregion

        #region Constructor
        public DeleteUserCommandHandler(
            IUnitOfWork unityOfWork, 
            IUserRepository userRepository,
            IArticleRepository articleRepository,
            IUserTokenRepository userTokenRepository,
            ISessionService sessionService
        )
        {
            this.unityOfWork = unityOfWork;        
            this.userRepository = userRepository;
            this.sessionService = sessionService;
            this.articleRepository = articleRepository;
            this.userTokenRepository = userTokenRepository;
        }
        #endregion

        #region Handle

        public async Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.Delete(request.ID);

            await Validate(user);            
            await userTokenRepository.DeleteAllTokensByUser(user.ID);
            await unityOfWork.Commit();
            
            return user;
        }

        private async Task Validate(User user)
        {
            var currentUser = await sessionService.GetCurrentUser();

            if(!currentUser.IsAdmin())
                throw new ForbiddenException("Sem permissão para acessar este recurso!");

            if (user is null) throw new NotFoundException("Usuário não existe!");

            var articles = await articleRepository.GetByUser(user.ID);

            if (articles.Count() != 0)
                throw new BadRequestException("Usuário possui artigos vinculados!");
        }
        #endregion
    }
    #endregion
}