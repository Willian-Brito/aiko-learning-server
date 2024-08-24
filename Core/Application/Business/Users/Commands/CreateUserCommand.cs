// using AikoLearning.Core.Application.DTOs;
// using AikoLearning.Core.Domain.Base;
// using AikoLearning.Core.Domain.Entities;
// using AikoLearning.Core.Domain.Interfaces;
// using AutoMapper;
// using MediatR;

// namespace AikoLearning.Core.Application.Users.Commands;

// public sealed class CreateUserCommand : IRequest<UserDTO>
// {
//     #region Command Properties    
//     public string Name { get; set; }
//     public string Password { get; set; }
//     public string Email { get; set; }
//     #endregion

//     #region Handler
//     public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDTO>
//     {
//         #region Properties
//         private readonly IMapper mapper;
//         private readonly IUnitOfWork unityOfWork;
//         private readonly IUserRepository userRepository;
//         private readonly IPasswordHasher passwordHasher;
//         #endregion

//         #region Constructor
//         public CreateUserCommandHandler(
//             IMapper mapper,
//             IUnitOfWork unityOfWork,
//             IUserRepository userRepository,
//             IPasswordHasher passwordHasher)
//         {
//             this.mapper = mapper;
//             this.unityOfWork = unityOfWork;
//             this.userRepository = userRepository;
//             this.passwordHasher = passwordHasher;
//         }
//         #endregion

//         #region Handle
//         public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
//         {
//             var hasEmail = await userRepository.GetByEmail(request.Email) != null;
//             if (hasEmail)
//                 throw new Exception("O e-mail de usuário já existe");
            
//             var user = User.Create(request.Name, request.Password, request.Email, false, passwordHasher);

//             var model = await userRepository.Insert(user);
//             var dto = mapper.Map<UserDTO>(user);
//             await unityOfWork.Commit();            
//             dto.ID = model.ID;

//             return dto;
//         }
//         #endregion
//     }
//     #endregion
// }