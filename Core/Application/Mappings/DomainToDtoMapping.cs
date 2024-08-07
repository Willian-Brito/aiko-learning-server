﻿using AikoLearning.Core.Application.Categories.Commands;
using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Entities;
using AutoMapper;

namespace AikoLearning.Core.Application.Mappings;

public class DomainToDtoMapping : Profile
{
    public DomainToDtoMapping()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        // CreateMap<Article, ArticleDTO>().ReverseMap();
    }
}
