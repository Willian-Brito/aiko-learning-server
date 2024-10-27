using System.ComponentModel.DataAnnotations.Schema;

namespace AikoLearning.Core.Domain.Model;

[Table("categories")]
public class Categories : AuditableEntity
{
    [Column("name")]
    public string Name { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }

    [ForeignKey(nameof(ParentId))]
    public Categories? Parent { get; set; }    
    public IEnumerable<Categories>? Children { get; set; }
    public IEnumerable<Articles>? Articles { get; set; }

    public Categories() { }
    public Categories(int id, string name, int? parentId, string userId) 
    {
        ID = id;        
        Name = name;
        ParentId = parentId;
        CreatedBy = userId;
    }
}