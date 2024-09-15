using System.ComponentModel.DataAnnotations.Schema;

namespace AikoLearning.Core.Domain.Model;

public class Stats
{
    [Column("_id")]
    public string ID { get; set; }

    [Column("users_count")]
    public int UsersCount { get; set; }

    [Column("categories_count")]
    public int CategoriesCount { get; set; }

    [Column("articles_count")]
    public int ArticlesCount { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    public Stats(int userCount, int categoriesCount, int articlesCount, DateTime createdAt) 
    {        
        UsersCount = userCount;        
        CategoriesCount = categoriesCount;
        ArticlesCount = articlesCount;
        CreatedAt = createdAt;
    }
}