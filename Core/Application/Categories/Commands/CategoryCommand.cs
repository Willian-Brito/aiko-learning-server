using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Entities;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Commands;

public abstract class CommandCategory : IRequest<CategoryDTO>
{
    public string Name { get; set; }
    public int? ParentId { get; set; }
}