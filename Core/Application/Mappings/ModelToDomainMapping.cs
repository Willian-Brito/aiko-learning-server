using AikoLearning.Core.Domain.Entities;
using Model = AikoLearning.Core.Domain.Model;
using AutoMapper;
using AikoLearning.Core.Domain.Account;

namespace AikoLearning.Core.Application.Mappings;

public class ModelToDomainMapping : Profile
{
    public ModelToDomainMapping()
    {
        CreateMap<Category, Model.Categories>().ReverseMap();
        CreateMap<Article, Model.Articles>().ReverseMap();
        CreateMap<User, Model.Users>().ReverseMap();
        CreateMap<UserToken, Model.UserTokens>().ReverseMap();
    }
}
