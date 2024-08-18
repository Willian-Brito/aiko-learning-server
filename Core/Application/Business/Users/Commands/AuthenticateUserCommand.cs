using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Account;
using AikoLearning.Core.Domain.Interfaces;
using MediatR;

namespace AikoLearning.Core.Application.Users.Commands;

public sealed class AuthenticateUserCommand : IRequest<UserTokenDTO>
{
    #region Properties Command
    public string Email { get; set; }
    public string Password { get; set; }
    #endregion

    #region Handler
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, UserTokenDTO>
    {
        #region Properties
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserToken userToken;
        #endregion

        #region Constructor
        public AuthenticateUserCommandHandler(
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher,
            IUserToken userToken
        )
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.userToken = userToken;
        }
        #endregion

        #region Handle
        public async Task<UserTokenDTO> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmail(request.Email);

            if (user == null || !passwordHasher.VerifyPassword(request.Password, user.Password))
                throw new ApplicationException("Usuário ou senha inválido");
            
            var dto = userToken.Generate(request.Email);
            return dto;
        }
        #endregion
    }
    #endregion    
}