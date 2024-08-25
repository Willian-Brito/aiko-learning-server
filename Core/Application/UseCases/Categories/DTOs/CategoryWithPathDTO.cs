namespace AikoLearning.Core.Application.DTOs;

public class CategoryWithPathDTO
{
    public int ID { get; set; }
    
    public string? Name { get; set; }

    public int? ParentId { get; set; }

    public string? Path { get; set; }
}