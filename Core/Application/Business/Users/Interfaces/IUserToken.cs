using AikoLearning.Core.Application.DTOs;

namespace AikoLearning.Core.Domain.Account;

public interface IUserToken
{
    UserTokenDTO Generate(string email);
}