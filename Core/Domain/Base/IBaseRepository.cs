namespace AikoLearning.Core.Domain.Base;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(int id);
    Task<T> Insert(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(int id);
}
