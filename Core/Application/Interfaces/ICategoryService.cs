using AikoLearning.Core.Application.Base;
using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Entities;

namespace AikoLearning.Core.Application.Interfaces;

public interface ICategoryService: IBaseService<Category, CategoryDTO>
{
    Task<CategoryDTO> GetByName(string name);
}
