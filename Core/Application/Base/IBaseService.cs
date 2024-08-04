namespace AikoLearning.Core.Application.Base;

public interface IBaseService<TEntity, TDto> 
    where TEntity : class 
    where TDto : class
{
    Task<IEnumerable<TDto>> GetAll();
    Task<TDto> Get(int id);
    Task<TDto> Insert(TDto dto);
    Task<TDto> Update(TDto dto);
    Task Delete(int id);
}
