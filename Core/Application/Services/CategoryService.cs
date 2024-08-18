// using AikoLearning.Core.Application.Base;
// using AikoLearning.Core.Application.DTOs;
// using AikoLearning.Core.Application.Interfaces;
// using AikoLearning.Core.Domain.Entities;
// using AikoLearning.Core.Domain.Interfaces;
// using AutoMapper;

// namespace AikoLearning.Core.Application.Services;

// public class CategoryService : BaseService<Category, CategoryDTO>, ICategoryService
// {
//     #region Properties
//     protected readonly ICategoryRepository categoryRepository;
//     private readonly IMapper mapper;
//     #endregion

//     #region Constructor
//     public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) 
//     : base(categoryRepository, mapper)
//     {
//         this.categoryRepository = categoryRepository;
//         this.mapper = mapper;
//     }
//     #endregion

//     #region Methods
//     public async Task<CategoryDTO> GetByName(string name)
//     {
//         var category = await categoryRepository.GetByName(name);
//         var categoryDTO = mapper.Map<CategoryDTO>(category);

//         return categoryDTO;
//     }
//     #endregion
// }
