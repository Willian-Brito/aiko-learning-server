using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Account;
using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Enums;
using AikoLearning.Core.Domain.Exceptions;
using AikoLearning.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AikoLearning.Core.Application.Users.Commands;

public sealed class CreateUserCommand : UserCommand
{
    #region Handler
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDTO>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly IRoleService roleService;
        private readonly IUnitOfWork unityOfWork;
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;
        #endregion

        #region Constructor
        public CreateUserCommandHandler(
            IMapper mapper,
            IUnitOfWork unityOfWork,
            IRoleService roleService,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher
        )
        {
            this.mapper = mapper;
            this.unityOfWork = unityOfWork;
            this.roleService = roleService;
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }
        #endregion

        #region Handle
        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var hasEmail = await userRepository.GetByEmail(request.Email) != null;
            if (hasEmail)
                throw new BadRequestException("O e-mail de usuário já existe");
            
            var roles = roleService.Convert(request.Roles);
            
            var user = User.Create
            (
                request.Name, 
                request.Password, 
                request.ConfirmPassword, 
                request.Email, 
                roles,
                passwordHasher
            );

            var model = await userRepository.Insert(user);
            var dto = mapper.Map<UserDTO>(user);
            await unityOfWork.Commit();            
            dto.ID = model.ID;

            return dto;
        }
        #endregion
    }
    #endregion
}