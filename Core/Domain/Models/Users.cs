using System.ComponentModel.DataAnnotations.Schema;
using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Enums;

namespace AikoLearning.Core.Domain.Model;

[Table("users")]
public class Users : AuditableEntity
{
    [Column("name")]
    public string Name { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("roles")]
    public List<Role> Roles { get; private set; }
    
    public ICollection<UserTokens> UserTokens { get; set; }
    public ICollection<Articles> Articles { get; set; }
    
    public Users() { }

    public Users(int id, string name, string password, string email, List<Role> roles, string createdBy) 
    { 
        ID = id;        
        Name = name;
        Password = password;
        Email = email;
        Roles = roles;
        CreatedBy = createdBy;
    }
}