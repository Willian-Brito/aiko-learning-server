using System.ComponentModel.DataAnnotations.Schema;
using AikoLearning.Core.Domain.Base;

namespace AikoLearning.Core.Domain.Model;

[Table("categories")]
public class Categories : BaseModel
{
    [Column("name")]
    public string Name { get; set; }

    [Column("parent_id")]
    // [ForeignKey(nameof(Parent))]
    public int? ParentId { get; set; }

    [ForeignKey(nameof(ParentId))]
    public Categories? Parent { get; set; }    
    public IEnumerable<Categories>? Children { get; set; }
    public IEnumerable<Articles>? Articles { get; set; }

    public Categories() { }
    public Categories(int id, string name, int? parentId) 
    {
        ID = id;        
        Name = name;
        ParentId = parentId;        
    }
}