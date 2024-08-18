using System.Data;

namespace AikoLearning.Core.Domain.Base;

public interface IUnitOfWork
{
    Task Commit();
    void Rollback();
}
