namespace AikoLearning.Core.Application.DTOs;

public class StatDTO
{
    public string ID { get; set; }
    public int UsersCount { get; set; }
    public int CategoriesCount { get; set; }
    public int ArticlesCount { get; set; }
    public DateTime CreatedAt { get; set; }

    public StatDTO() { }
}
