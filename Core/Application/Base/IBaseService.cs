using AikoLearning.Core.Domain.Base;

namespace AikoLearning.Core.Application.Base;

public interface IBaseService<TEntity, TModel> : IBaseRepository<TEntity, TModel>
    where TEntity : class 
    where TModel : class
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> Get(int id);
    Task<TModel> Insert(TEntity model);
    Task<TEntity> Update(TEntity model);
    Task<TEntity> Delete(int id);
}
