using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Account;
using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Users.Commands;

public sealed class UpdateUserCommand : UserCommand
{
    #region Command Properties
    public int ID { get; set; }
    #endregion

    #region Handler
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDTO>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly IRoleService roleService;
        private readonly IUnitOfWork unityOfWork;
        private readonly IUserRepository userRepository;
        #endregion

        #region Constructor
        public UpdateUserCommandHandler(
            IMapper mapper,
            IUnitOfWork unityOfWork, 
            IRoleService roleService,
            IUserRepository userRepository
        )
        {
            this.mapper = mapper;
            this.unityOfWork = unityOfWork;        
            this.roleService = roleService;        
            this.userRepository = userRepository;
        }
        #endregion

        #region Handle

        public async Task<UserDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(request.ID);

            if (user == null)
                throw new InvalidOperationException("Usuário não existe!");
            
            var roles = roleService.Convert(request.Roles);

            user.Update
            (
                request.Name, 
                request.Password,
                request.Email,
                roles
            );

            await userRepository.Update(user);
            var dto = mapper.Map<UserDTO>(user);

            await unityOfWork.Commit();
            return dto;
        }
        #endregion
    }
    #endregion
}