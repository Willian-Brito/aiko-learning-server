using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AikoLearning.Infrastructure.Data.MongoDB.Models;

public class Stats
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set;}

    [BsonElement("users_count")]
    public int UsersCount { get; set; }

    [BsonElement("categories_count")]
    public int CategoriesCount { get; set; }

    [BsonElement("articles_count")]
    public int ArticlesCount { get; set; }

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }
    
    public Stats(int userCount, int categoriesCount, int articlesCount, DateTime created_at) 
    {
        UsersCount = userCount;        
        CategoriesCount = categoriesCount;
        ArticlesCount = articlesCount;
        CreatedAt = created_at;
    }

    public Stats(string id, int userCount, int categoriesCount, int articlesCount, DateTime created_at) 
    {
        ID = id;
        UsersCount = userCount;        
        CategoriesCount = categoriesCount;
        ArticlesCount = articlesCount;
        CreatedAt = created_at;
    }
}