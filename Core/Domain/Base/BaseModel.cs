using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AikoLearning.Core.Domain.Base;

public abstract class BaseModel
{
    [Key, Column("id")]    
    public int ID { get; set; }

    // [Column("created_at")]
    // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    // public DateTime CreatedAt { get; protected set; }
    
    // [Column("created_by")]
    // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    // public int? CreatedBy { get; protected set; }
    
    // [Column("updated_at")]
    // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    // public DateTime? UpdatedAt { get; protected set; }

    // [Column("updated_by")]    
    // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    // public int? UpdatedBy { get; protected set; }
    
    // [Column("deleted_at")]
    // public DateTime? DeletedAt { get; protected set; }

    // [Column("deleted_by")]    
    // public int? DeletedBy { get; protected set; }
}