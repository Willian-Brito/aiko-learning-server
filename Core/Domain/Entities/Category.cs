using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Validations;

namespace AikoLearning.Core.Domain.Entities;

public sealed class Category : BaseEntity
{
    #region Properties
    public string Name { get; private set; }
    public int? ParentId { get; private set; }
    public Category? Parent { get; set; }
    public ICollection<Category>? Children { get; set; }
    public ICollection<Article>? Articles { get; set; }
    #endregion

    #region  Constructors
    public Category(string name, int? parentId)
    {
        Create(name, parentId);
    }

    public Category(int id, string name, int? parentId)
    {
        DomainValidationException.When(id < 0, "id da categoria inválido!");
        ID = id;        
        Update(name, parentId);
    }
    #endregion

    #region Methods

    private void Create(string name, int? parentId)
    {
        Validate(name);
        Name = name;
        ParentId = GetParentId(parentId);        
    }

    public void Update(string name, int? parentId)
    {
        Validate(name);
        Name = name;
        ParentId = GetParentId(parentId);
    }

    private int? GetParentId(int? parentId)
    {
        return parentId == 0 ? null : parentId;
    }

    private void Validate(string name)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), "Informe o nome!");
        DomainValidationException.When(name.Length < 3, "Nome inválido, é necessário ter no minimo 3 caracteres!");
        DomainValidationException.When(name.Length > 100, "Nome deve ser menor que 100 caracteres!");
    }
    #endregion
}