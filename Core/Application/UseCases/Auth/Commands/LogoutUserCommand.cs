// namespace AikoLearning.Core.Application.Auth.Commands;

// public class LogoutUserCommand : IRequest<Unit>
// {
//     public string Token { get; set; }
// }

// public class LogoutUserCommandHandler : IRequestHandler<LogoutCommand, Unit>
// {
//     private readonly IRevokedTokenRepository _revokedTokenRepository;
//     private readonly ITokenService _tokenService;

//     public LogoutUserCommandHandler(IRevokedTokenRepository revokedTokenRepository, ITokenService tokenService)
//     {
//         _revokedTokenRepository = revokedTokenRepository;
//         _tokenService = tokenService;
//     }

//     public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
//     {
//         var expiryDate = _tokenService.GetExpiryDateFromToken(request.Token);

//         if (expiryDate > DateTime.UtcNow)
//         {
//             var revokedToken = new RevokedToken
//             {
//                 Token = request.Token,
//                 ExpiryDate = expiryDate
//             };
            
//             await _revokedTokenRepository.AddRevokedTokenAsync(revokedToken);
//         }

//         return Unit.Value;
//     }
// }
