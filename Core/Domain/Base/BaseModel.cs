using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AikoLearning.Core.Domain.Base;

public abstract class BaseModel
{
    [Key, Column("id")]    
    public int ID { get; set; }
}