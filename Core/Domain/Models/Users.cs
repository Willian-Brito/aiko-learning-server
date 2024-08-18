using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using AikoLearning.Core.Domain.Base;

namespace AikoLearning.Core.Domain.Model;

[Table("users")]
public class Users : BaseModel
{
    [Column("name")]
    public string Name { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("is_admin")]
    public bool IsAdmin { get; set; } = false;
    
    public ICollection<Articles> Articles { get; set; }
    
    public Users() { }

    public Users(int id, string name, string password, string email, bool isAdmin) 
    { 
        ID = id;        
        Name = name;
        Password = password;
        Email = email;
        IsAdmin = isAdmin;        
    }
}