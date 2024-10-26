using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Enums;
using AikoLearning.Core.Domain.Exceptions;
using FluentAssertions;
using Moq;

namespace TestUnit.Domain;

public class UserUnitTest
{
    #region ID
    [Fact(DisplayName = "Não deve criar usuário quando o id for negativo")]
    public void CreateUser_NegativeIdValue_DomainExceptionInvalidId()
    {
        var action = () => 
            new User
            (
                id: -1,
                name: "Willian Brito",
                password: "$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK", 
                email: "wbrito@aiko.digital",
                roles: new List<Role> { Role.Administrator }
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("id do usuário inválido!");
    }
    #endregion

    #region User
    [Fact(DisplayName = "Criar usuário com o estado válido")]
    public void CreateUser_WithValidParameters_ResultObjectValidState()
    {
        var action = () => 
            new User
            (
                id: 1,
                name: "Willian Brito",
                password: "$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK", 
                email: "wbrito@aiko.digital",
                roles: new List<Role> { Role.Administrator }
            );

        action.Should().NotThrow<DomainValidationException>();
    }
    #endregion

    #region Name

    [Fact(DisplayName = "Não deve criar usuário quando o nome for menor que 3 caracteres")]
    public void CreateUser_ShortNameValue_DomainExceptionRequiredName()
    {
        var action = () =>
             new User
            (
                id: 1,
                name: "Wi",
                password: "$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK", 
                email: "wbrito@aiko.digital",
                roles: new List<Role> { Role.Administrator }
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Nome inválido, é necessário ter no minimo 3 caracteres!");
    }

    [Fact(DisplayName = "Não deve criar usuário quando o nome for vazio")]
    public void CreateUser_MissingNameValue_DomainExceptionRequiredName()
    {
        var action = () => 
             new User
            (
                id: 1,
                name: "",
                password: "$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK", 
                email: "wbrito@aiko.digital",
                roles: new List<Role> { Role.Administrator }
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe o nome!");
    }

    [Fact(DisplayName = "Não deve criar usuário quando o nome for nulo")]
    public void CreateUser_WithNameValue_DomainExceptionInvalidName()
    {
        var action = () => 
             new User
            (
                id: 1,
                name: null,
                password: "$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK", 
                email: "wbrito@aiko.digital",
                roles: new List<Role> { Role.Administrator }
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe o nome!");
    }

    [Fact(DisplayName = "Não deve criar usuário quando o nome for maior que 200 caracteres")]
    public void CreateUser_WithNameInvalid_DomainExceptionInvalidName()
    {
        var name = @"testeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
        eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeetesteeeeeeeeeeeeeeee
        eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee";

        var action = () => 
             new User
            (
                id: 1,
                name: name,
                password: "$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK", 
                email: "wbrito@aiko.digital",
                roles: new List<Role> { Role.Administrator }
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Nome deve ser menor que 200 caracteres!");
    }
    #endregion

    #region Password
    [Fact(DisplayName = "Não deve criar usuário quando a senha for vazia")]
    public void CreateUser_WithPasswordIsEmpty_DomainExceptionInvalidPassword()
    {
        var mockUserService = new Mock<IPasswordHasher>();
        
        mockUserService.Setup(service => service.EncryptPassword("123456"))
                       .Returns("$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK");

        var action = () => 
            User.Create
            (                
                name: "Willian Brito",
                password: "",
                confirmPassword: "123456",
                email: "wbrito@aiko.digital",
                roles: new List<Role> { Role.Administrator },
                mockUserService.Object
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe a senha!");
    }

    [Fact(DisplayName = "Não deve criar usuário quando a senha for nula")]
    public void CreateUser_WithPasswordIsNulll_DomainExceptionInvalidPassword()
    {
        var mockUserService = new Mock<IPasswordHasher>();
        
        mockUserService.Setup(service => service.EncryptPassword("123456"))
                       .Returns("$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK");

        var action = () => 
            User.Create
            (                
                name: "Willian Brito",
                password: null,
                confirmPassword: "123456",
                email: "wbrito@aiko.digital",
                roles: new List<Role> { Role.Administrator },
                mockUserService.Object
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe a senha!");
    }
    #endregion

    #region ConfirmPassword
    [Fact(DisplayName = "Não deve criar usuário quando a confirmação da senha for vazia")]
    public void CreateUser_WithConfirmPasswordIsEmpty_DomainExceptionInvalidConfirmPassword()
    {
        var mockUserService = new Mock<IPasswordHasher>();
        
        mockUserService.Setup(service => service.EncryptPassword("123456"))
                       .Returns("$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK");

        var action = () => 
            User.Create
            (                
                name: "Willian Brito",
                password: "123456",
                confirmPassword: "",
                email: "wbrito@aiko.digital",
                roles: new List<Role> { Role.Administrator },
                mockUserService.Object
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe a confirmação de senha!");
    }

    [Fact(DisplayName = "Não deve criar usuário quando a confirmação da senha for nula")]
    public void CreateUser_WithConfirmPasswordIsNulll_DomainExceptionInvalidConfirmPassword()
    {
        var mockUserService = new Mock<IPasswordHasher>();
        
        mockUserService.Setup(service => service.EncryptPassword("123456"))
                       .Returns("$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK");

        var action = () => 
            User.Create
            (                
                name: "Willian Brito",
                password: "123456",
                confirmPassword: null,
                email: "wbrito@aiko.digital",
                roles: new List<Role> { Role.Administrator },
                mockUserService.Object
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe a confirmação de senha!");
    }
    #endregion

    #region Passwords Don't Match
    [Fact(DisplayName = "Não deve criar usuário quando a senha e confirmação de senha sejam diferentes")]
    public void CreateUser_WithPasswordsDontMatch_DomainExceptionInvalidPassword()
    {
        var mockUserService = new Mock<IPasswordHasher>();
        
        mockUserService.Setup(service => service.EncryptPassword("123456"))
                       .Returns("$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK");

        var action = () => 
            User.Create
            (                
                name: "Willian Brito",
                password: "123456",
                confirmPassword: "12345",
                email: "wbrito@aiko.digital",
                roles: new List<Role> { Role.Administrator },
                mockUserService.Object
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Senhas não conferem!");
    }
    #endregion
}
