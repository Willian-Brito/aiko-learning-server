namespace AikoLearning.Core.Domain.Base;

public interface IBaseRepository<TEntity, TModel>
    where TEntity : class
    where TModel : class
{
    Task<IEnumerable<TEntity>> GetPaged(int pageIndex, int pageSize);
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> Get(int id);
    Task<TModel> Insert(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task<TEntity> Delete(int id);
    Task<int> Count();
}
