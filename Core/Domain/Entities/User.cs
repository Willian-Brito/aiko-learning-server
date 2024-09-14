using System.Collections.ObjectModel;
using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Enums;
using AikoLearning.Core.Domain.ValuesObjects;
using AikoLearning.Core.Domain.Exceptions;

namespace AikoLearning.Core.Domain.Entities;

public sealed class User : BaseEntity
{
    #region Properties
    public string Name { get; private set; }
    public string Password { get; private set; }
    public Email Email { get; private set; }    
    public List<Role> Roles { get; private set; }
    public List<Article> Articles { get; set; }
    #endregion

    #region Constructors
    public User(
        string name, 
        string password, 
        string email,
        List<Role> roles    
    )
    {
        Validade(name, roles);
        SetAtributes(name, password, email, roles);
    }

    public User(
        int id,
        string name, 
        string password, 
        string email, 
        List<Role> roles
    )
    {
        DomainValidationException.When(id < 0, "id do usuário inválido!");
        ID = id;
        Validade(name, roles);
        SetAtributes(name, password, email, roles);
    }
    #endregion

    #region Method

    public static User Create(string name, string password, string confirmPassword, string email, List<Role> roles, IPasswordHasher passwordHasher)
    {
        DomainValidationException.When(password != confirmPassword, "Senhas não conferem!");

        var hash = passwordHasher.EncryptPassword(password);
        return new User(name, hash, email, roles);
    }

    public void Update(
        string name, 
        string password, 
        string email, 
        List<Role> roles,
        IPasswordHasher passwordHasher
    )
    {
        var hash = passwordHasher.EncryptPassword(password);
        Validade(name, roles);
        SetAtributes(name, hash, email, roles);
    }

    public bool IsAdmin()
    {
        var isAdmin = Roles.Any(r => r is Role.Administrator);
        return isAdmin;
    }

    private void Validade(string name, List<Role> roles)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), "Informe o nome!");
        DomainValidationException.When(name.Length < 3, "Nome inválido, é necessário ter no minimo 3 caracteres!");
        DomainValidationException.When(name.Length > 200, "Nome deve ser menor que 200 caracteres!");
        
        var allRoles = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
        var isValid = roles.Any(role => !allRoles.Contains(role));

        DomainValidationException.When(isValid, "Regra de perfil inválida!");
    }

    private void SetAtributes(
        string name, 
        string hash, 
        string email, 
        List<Role> roles
    )
    {
        Name = name;
        Password = hash;
        Email = new Email(email);
        Roles = roles;
    }
    #endregion
}