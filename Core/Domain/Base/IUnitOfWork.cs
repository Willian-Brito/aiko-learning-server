using System.Data;

namespace AikoLearning.Core.Domain.Base;

public interface IUnitOfWork
{
    // IDbConnection Connection { get; }    
    // IDbTransaction BeginTransaction();
    Task Commit();
    void Rollback();
}
