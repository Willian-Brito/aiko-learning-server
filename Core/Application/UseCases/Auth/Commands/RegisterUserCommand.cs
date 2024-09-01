using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Account;
using AikoLearning.Core.Domain.Base;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Auth.Commands;

public sealed class RegisterUserCommand : IRequest<UserDTO>
{
    #region Command Properties
    public string Name { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
    #endregion

    #region Handler
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDTO>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly IUnitOfWork unityOfWork;
        private readonly IAuthenticateService authenticateService;
        #endregion

        #region Constructor
        public RegisterUserCommandHandler(
            IMapper mapper,
            IUnitOfWork unityOfWork,
            IAuthenticateService authenticateService
        )
        {
            this.mapper = mapper;
            this.unityOfWork = unityOfWork;
            this.authenticateService = authenticateService;
        }
        #endregion

        #region Handle
        public async Task<UserDTO> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = await authenticateService.Register
            (
                request.Name,
                request.Password,
                request.ConfirmPassword,
                request.Email
            );

            var dto = mapper.Map<UserDTO>(user);
            dto.Roles = user.Roles.Select(r => r.ToString()).ToArray();
            
            await unityOfWork.Commit();
            dto.ID = user.ID;

            return dto;
        }
        #endregion
    }
    #endregion
}