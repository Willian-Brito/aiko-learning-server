using System.Text.Json.Serialization;
using AikoLearning.Core.Application.DTOs;
using MediatR;

namespace AikoLearning.Core.Application.Categories.Commands;

public abstract class ArticleCommand : IRequest<ArticleDTO>
{
    public string? Name { get; set; }
    public int? CategoryId { get; set; }
    public int? UserId { get; set; }
    public string? Description { get; set; }
    public byte[]? Content { get; set; }
    public string? ImageUrl { get; set; }
}