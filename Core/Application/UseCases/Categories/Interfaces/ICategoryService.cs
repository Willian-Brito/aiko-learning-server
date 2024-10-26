using AikoLearning.Core.Application.Base;
using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Entities;
using Model = AikoLearning.Core.Domain.Model;

namespace AikoLearning.Core.Application.Interfaces;

public interface ICategoryService : IBaseService<Category, Model.Categories>
{
    Task<CategoryDTO> GetByName(string name);
    Task<string> GetPath(int id);
    Task<List<CategoryDTO>> GetTree(int id);
}
