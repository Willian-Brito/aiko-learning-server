using AikoLearning.Core.Domain.Model;

namespace AikoLearning.Core.Domain.Interfaces;

public interface IStatRepository
{
    Task<Stats> GetLast();
    Task<List<Stats>> GetAll();
    Task<Stats> Create(Stats entity);
}
