using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Model;
using AutoMapper;

namespace Application;

public class ModelToDtoMapping : Profile
{
    public ModelToDtoMapping()
    {
        CreateMap<Categories, CategoryDTO>().ReverseMap();
        CreateMap<Articles, ArticleDTO>().ReverseMap();
        CreateMap<Users, UserDTO>().ReverseMap();
    }
}
