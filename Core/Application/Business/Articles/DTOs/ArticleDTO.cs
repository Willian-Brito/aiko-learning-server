using System.Text.Json.Serialization;
using AikoLearning.Core.Domain.Entities;

namespace AikoLearning.Core.Application.DTOs;

public class ArticleDTO 
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }    

    [JsonIgnore]
    public byte[]? Content { get; set; }

    [JsonIgnore]
    public Category Category { get; set; }

    public ArticleDTO() { }
}