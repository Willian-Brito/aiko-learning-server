using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Exceptions;
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
                isAdmin: true
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
                isAdmin: true
            );

        action.Should().NotThrow<DomainValidationException>();
    }
    #endregion

    #region Name

    [Fact(DisplayName = "Não deve criar usuário quando o nome for menor que 3 caracteres")]
    public void CreateArticle_ShortNameValue_DomainExceptionRequiredName()
    {
        var action = () =>
             new User
            (
                id: 1,
                name: "Wi",
                password: "$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK", 
                email: "wbrito@aiko.digital",
                isAdmin: true
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Nome inválido, é necessário ter no minimo 3 caracteres!");
    }

    [Fact(DisplayName = "Não deve criar usuário quando o nome for vazio")]
    public void CreateArticle_MissingNameValue_DomainExceptionRequiredName()
    {
        var action = () => 
             new User
            (
                id: 1,
                name: "",
                password: "$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK", 
                email: "wbrito@aiko.digital",
                isAdmin: true
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe o nome!");
    }

    [Fact(DisplayName = "Não deve criar usuário quando o nome for nulo")]
    public void CreateArticle_WithNameValue_DomainExceptionInvalidName()
    {
        var action = () => 
             new User
            (
                id: 1,
                name: null,
                password: "$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK", 
                email: "wbrito@aiko.digital",
                isAdmin: true
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe o nome!");
    }

    [Fact(DisplayName = "Não deve criar usuário quando o nome for maior que 200 caracteres")]
    public void CreateArticle_WithNameInvalid_DomainExceptionInvalidName()
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
                isAdmin: true
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Nome deve ser menor que 200 caracteres!");
    }
    #endregion
}