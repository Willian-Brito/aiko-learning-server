using System.Collections.ObjectModel;
using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.ValuesObjects;
using AikoLearning.Core.Exceptions;

namespace AikoLearning.Core.Domain.Entities;

public sealed class User : BaseEntity
{
    #region Properties
    public string Name { get; private set; }
    public string Password { get; private set; }
    public Email Email { get; private set; }
    public bool IsAdmin { get; private set; }
    public Collection<Article> Articles { get; set; }
    #endregion

    #region Constructors
    public User(
        string name, 
        string password, 
        string email, 
        bool isAdmin        
    )
    {
        Validade(name);
        SetAtributes(name, password, email, isAdmin);
    }

    public User(
        int id,
        string name, 
        string password, 
        string email, 
        bool isAdmin        
    )
    {
        DomainValidationException.When(id < 0, "id do usuário inválido!");
        ID = id;
        Validade(name);
        SetAtributes(name, password, email, isAdmin);
    }
    #endregion

    #region Method

    public static User Create(string name, string password, string confirmPassword, string email, bool isAdmin, IPasswordHasher passwordHasher)
    {
        DomainValidationException.When(password != confirmPassword, "Senhas não conferem!");

        var hash = passwordHasher.EncryptPassword(password);
        return new User(name, hash, email, isAdmin);
    }

    private void Validade(string name)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), "Informe o nome!");
        DomainValidationException.When(name.Length < 3, "Nome inválido, é necessário ter no minimo 3 caracteres!");
        DomainValidationException.When(name.Length > 200, "Nome deve ser menor que 200 caracteres!");
    }

    private void SetAtributes(
        string name, 
        string hash, 
        string email, 
        bool isAdmin
    )
    {
        Name = name;
        Password = hash;
        Email = new Email(email);
        IsAdmin = isAdmin;
    }
    #endregion
}