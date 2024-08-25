using AikoLearning.Core.Domain.Base;
using AutoMapper;

namespace AikoLearning.Core.Application.Base;

public abstract class BaseService<TEntity, TModel> : IBaseService<TEntity, TModel> 
    where TEntity : class 
    where TModel : class
{
    #region Properties
    private readonly IBaseRepository<TEntity, TModel> repository;    
    #endregion

    #region Construtor
    public BaseService(IBaseRepository<TEntity, TModel> repository)
    {
        this.repository = repository;        
    }
    #endregion

    #region Methods

    public async Task<IEnumerable<TEntity>> GetPaged(int pageIndex, int pageSize)
    {
        return await repository.GetPaged(pageIndex, pageSize);
    }
    
    public async Task<TEntity> Get(int id)
    {        
        return await repository.Get(id);
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {        
        return await repository.GetAll();
    }

    public async Task<TModel> Insert(TEntity entity)
    {
        return await repository.Insert(entity);
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        return await repository.Update(entity);
    }

    public async Task<TEntity> Delete(int id)
    {
        return await repository.Delete(id);
    }

    public async Task<int> Count()
    {
        return await repository.Count();
    }
    #endregion

}
