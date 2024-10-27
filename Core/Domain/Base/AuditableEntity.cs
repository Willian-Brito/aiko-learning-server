using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AikoLearning.Core.Domain.Base;

public abstract class AuditableEntity : IAuditableEntity
{
    [Key, Column("id")]    
    public int ID { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("created_by")]
    public string CreatedBy { get; set; }
    
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]    
    public string? UpdatedBy { get; set; }
    
    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [Column("deleted_by")]    
    public string? DeletedBy { get; set; }
}
