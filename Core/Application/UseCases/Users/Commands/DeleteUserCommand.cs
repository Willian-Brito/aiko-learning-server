using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Entities;
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
        private readonly IArticleRepository articleRepository;
        #endregion

        #region Constructor
        public DeleteUserCommandHandler(
            IUnitOfWork unityOfWork, 
            IUserRepository userRepository,
            IArticleRepository articleRepository
        )
        {
            this.unityOfWork = unityOfWork;        
            this.userRepository = userRepository;
            this.articleRepository = articleRepository;
        }
        #endregion

        #region Handle

        public async Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.Delete(request.ID);     

            if (user == null)
                throw new InvalidOperationException("Artigo não existe!");
                            
            await unityOfWork.Commit();
            return user;
        }
        #endregion
    }
    #endregion
}