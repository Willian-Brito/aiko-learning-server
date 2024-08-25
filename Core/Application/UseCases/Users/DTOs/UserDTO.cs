using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using AikoLearning.Core.Domain.Entities;

namespace AikoLearning.Core.Application.DTOs;

public class UserDTO
{
    public int ID { get; set; }
    public string Name { get; set; }    
    public string Email { get; set; }
    public bool IsAdmin { get; set; }

    [JsonIgnore]
    public Collection<Article> Articles { get; set; }

    public UserDTO() { }
}
