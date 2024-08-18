using AikoLearning.Core.Application.DTOs;
using MediatR;

namespace AikoLearning.Core.Application.Commands;

public class UserCommand : IRequest<UserDTO>
{    
    public string ID { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; }
}
