using AikoLearning.Core.Domain.Enums;

namespace AikoLearning.Core.Domain.Account;

public interface IRoleService
{
    List<Role> GetAllRoles();
    List<Role> Convert(List<int> roles);
    List<string> GetAllRolesNames();
    List<string> GetNamesByRoles(List<Role> roles);
}
