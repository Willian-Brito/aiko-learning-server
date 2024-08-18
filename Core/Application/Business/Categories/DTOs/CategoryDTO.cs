using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AikoLearning.Core.Domain.Entities;

namespace AikoLearning.Core.Application.DTOs;

public class CategoryDTO
{
    public int? ID { get; set; }
    
    public string? Name { get; set; }

    public long ParentId { get; set; }
    
    [JsonIgnore]
    public Category? Parent { get; set; }

    [JsonIgnore]
    public ICollection<Category>? Children { get; set; }

    public CategoryDTO() { }
}
