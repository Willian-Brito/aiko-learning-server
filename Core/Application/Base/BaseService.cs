using AikoLearning.Core.Domain.Base;
using AutoMapper;

namespace AikoLearning.Core.Application.Base;

public abstract class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto> 
    where TEntity : class 
    where TDto : class
{
    #region Properties
    private readonly IBaseRepository<TEntity, TDto> repository;
    private readonly IMapper mapper;
    #endregion

    #region Construtor
    public BaseService(
        IBaseRepository<TEntity, TDto> repository,
        IMapper mapper
    )
    {
        this.repository = repository;
        this.mapper = mapper;
    }
    #endregion

    #region Methods
    public async Task<TDto> Get(int id)
    {
        var entity = await repository.Get(id);
        return mapper.Map<TDto>(entity);
    }

    public async Task<IEnumerable<TDto>> GetAll()
    {
        var entities = await repository.GetAll();
        return mapper.Map<IEnumerable<TDto>>(entities);
    }

    public async Task<TDto> Insert(TDto dto)
    {
        var entity = mapper.Map<TEntity>(dto);        
        await repository.Insert(entity);
        return dto;
    }

    public async Task<TDto> Update(TDto dto)
    {
        var entity = mapper.Map<TEntity>(dto);
        await repository.Update(entity);
        return dto;
    }

    public async Task Delete(int id)
    {
        await repository.Delete(id);
    }
    #endregion

}
