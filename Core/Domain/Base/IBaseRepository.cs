namespace AikoLearning.Core.Domain.Base;

public interface IBaseRepository<TEntity, TModel>
    where TEntity : class
    where TModel : class
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> Get(object id);
    Task<TModel> Insert(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task<TEntity> Delete(object id);
}
