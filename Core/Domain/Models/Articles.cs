using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AikoLearning.Core.Domain.Base;

namespace AikoLearning.Core.Domain.Model;

[Table("articles")]
public class Articles : BaseModel
{
    [Column("name")]
    public string Name { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("image_url")]
    public string? ImageUrl { get; set; }

    [Column("content")]    
    public byte[]? Content { get; set; }

    [NotMapped]    
    public string? Author { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Categories? Category { get; set; }

    [ForeignKey(nameof(UserId))]
    public Users? User { get; set; }

    public Articles() { }
    public Articles(
        int id,
        string name, 
        int categoryId, 
        int userId,
        string description, 
        string? imageUrl,
        byte[]? content
    )
    {
        ID = id;
        Name = name;
        CategoryId = categoryId;
        UserId = userId;
        Description = description;
        ImageUrl = imageUrl;
        Content = content;
    }
}