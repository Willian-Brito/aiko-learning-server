using AikoLearning.Core.Application.Base;
using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Application.Interfaces;
using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Interfaces;
using Model = AikoLearning.Core.Domain.Model;
using AutoMapper;

namespace AikoLearning.Core.Application.Services;

public class CategoryService : BaseService<Category, Model.Categories>, ICategoryService
{
    #region Properties
    protected readonly ICategoryRepository categoryRepository;    
    private readonly IMapper mapper;
    #endregion

    #region Constructor
    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)  
    : base(categoryRepository)  
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }
    #endregion

    #region Methods

    #region GetByName
    public async Task<CategoryDTO> GetByName(string name)
    {
        var category = await categoryRepository.GetByName(name);
        var categoryDTO = mapper.Map<CategoryDTO>(category);

        return categoryDTO;
    }
    #endregion

    #region GetPath
    public async Task<string> GetPath(int id)
    {            
        var category = await categoryRepository.Get(id);
        var parent = await categoryRepository.GetParent(category.ParentId);
        var path = category.Name;

        while(parent != null)
        {
            path = $"{parent.Name} > {path}";
            
            parent = parent.ParentId != null 
                ? await categoryRepository.GetParent(parent.ParentId)
                : null;
        }

        return path;
    }
    #endregion

    #region GetTree
    public async Task<CategoryDTO> GetTree(int id)
    {
        var categories = await categoryRepository.GetAll();
        var rootCategory = await categoryRepository.Get(id);
        
        if (rootCategory != null)
            await PopulateChildren(categories.ToList(), rootCategory);
        
        var dto = mapper.Map<CategoryDTO>(rootCategory);
        return dto;
    }
    
    private async Task PopulateChildren(List<Category> categories, Category parentCategory)
    {
        var children = categories.Where(c => c.ParentId == parentCategory.ID).ToList();

        foreach (var child in children)
        {
            parentCategory.Children.Add(child);
            PopulateChildren(categories, child);
        }
    }
    #endregion

    #endregion
}
