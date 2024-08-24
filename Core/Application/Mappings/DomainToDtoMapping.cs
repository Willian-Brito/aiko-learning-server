using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Entities;
using AutoMapper;

namespace AikoLearning.Core.Application.Mappings;

public class DomainToDtoMapping : Profile
{
    public DomainToDtoMapping()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Category, CategoryWithPathDTO>().ReverseMap();
        CreateMap<Article, ArticleDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
    }
}
