using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Validations;

namespace AikoLearning.Core.Domain.Entities;

public sealed class Article : BaseEntity
{
    #region Properties
    public string Name { get; private set; }
    public int CategoryId { get; private set; }
    public int UserId { get; private set; }
    public string Description { get; private set; }
    public string? ImageUrl { get; private set; }
    public byte[]? Content { get; private set; }
    public Category Category { get; set; }
    public User User { get; set; }
    #endregion

    #region Constructor
    public Article(
        string name, 
        int categoryId, 
        int userId,
        string description, 
        string? imageUrl,
        byte[]? content
    )
    {
        Update(name, categoryId, userId, description, imageUrl, content);        
    }

    public Article(
        int id,
        string name, 
        int categoryId, 
        int userId,
        string description, 
        string? imageUrl,
        byte[]? content
    )
    {
        DomainValidationException.When(id < 0, "id da categoria inválido!");
        ID = id;        
        Update(name, categoryId, userId, description, imageUrl, content);
    }
    #endregion

    #region Methods

    public void Update(
        string name, 
        int categoryId, 
        int userId,
        string description, 
        string? imageUrl,
        byte[]? content
    )
    {
        Validade(name, categoryId, userId, description);
        
        Name = name;
        CategoryId = categoryId;
        UserId = userId;
        Description = description;
        ImageUrl = imageUrl;
        Content = content; 
    }

    private void Validade(
        string name, 
        int categoryId, 
        int userId,
        string description
    )
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), "Informe o nome!");
        DomainValidationException.When(name.Length < 3, "Nome inválido, é necessário ter no minimo 3 caracteres!");

        DomainValidationException.When(categoryId < 0, "id da categoria inválido!");
        DomainValidationException.When(userId < 0, "id do usuáario inválido!");

        DomainValidationException.When(description.Length > 1000, "Descrição deve ser menor que 1000 caracteres!");
    }
    #endregion
}
