using AikoLearning.Core.Application.Auth.Interfaces;
using MediatR;

namespace AikoLearning.Core.Application.Auth.Commands;

public sealed class ValidateTokenCommand : IRequest<bool>
{
    #region Command Properties
    public string Token { get; set; }    
    #endregion

    #region Handler
    public class ValidateTokenCommandHandler : IRequestHandler<ValidateTokenCommand, bool>
    {
        #region Properties
        private readonly ITokenService tokenService;
        #endregion

        #region Constructor
        public ValidateTokenCommandHandler(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }
        #endregion

        #region Handle
        public async Task<bool> Handle(ValidateTokenCommand request, CancellationToken cancellationToken)
        {
            return tokenService.IsValidToken(request.Token);
        }
        #endregion
    }
    #endregion
}