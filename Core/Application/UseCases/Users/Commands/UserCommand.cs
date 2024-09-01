using AikoLearning.Core.Application.DTOs;
using MediatR;

namespace AikoLearning.Core.Application.Users.Commands;

public abstract class UserCommand : IRequest<UserDTO>
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
    public List<int> Roles { get; set; }
}