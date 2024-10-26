using System.Text;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Exceptions;
using FluentAssertions;

namespace TestUnit.Domain;

public class ArticleUnitTest
{
    #region ID
    [Fact(DisplayName = "Não deve criar artigo quando o id for negativo")]
    public void CreateArticle_NegativeIdValue_DomainExceptionInvalidId()
    {
        var action = () => 
            new Article
            (
                id: -1,
                name: "Article Name", 
                categoryId: 1, 
                userId: 1, 
                description: "Article Description", 
                content:  Encoding.UTF8.GetBytes("teste"),
                imageUrl: "Article ImageUrl"
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("id do artigo inválido!");
    }
    #endregion

    #region Article
    [Fact(DisplayName = "Criar artigo com o estado válido")]
    public void CreateArticle_WithValidParameters_ResultObjectValidState()
    {
        var action = () => 
            new Article
            (
                id: 1,
                name: "Article Name", 
                categoryId: 1, 
                userId: 1, 
                description: "Article Description", 
                content:  Encoding.UTF8.GetBytes("teste"),
                imageUrl: "Article ImageUrl"
            );

        action.Should().NotThrow<DomainValidationException>();
    }
    #endregion

    #region Name

    [Fact(DisplayName = "Não deve criar artigo quando o nome for menor que 3 caracteres")]
    public void CreateArticle_ShortNameValue_DomainExceptionRequiredName()
    {
        var action = () =>
            new Article
            (
                id: 1,
                name: "Ar", 
                categoryId: 1, 
                userId: 1, 
                description: "Article Description", 
                content:  Encoding.UTF8.GetBytes("teste"),
                imageUrl: "Article ImageUrl"
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Nome inválido, é necessário ter no minimo 3 caracteres!");
    }

    [Fact(DisplayName = "Não deve criar artigo quando o nome for vazio")]
    public void CreateArticle_MissingNameValue_DomainExceptionRequiredName()
    {
        var action = () => 
            new Article
            (
                id: 1,
                name: "", 
                categoryId: 1, 
                userId: 1, 
                description: "Article Description", 
                content:  Encoding.UTF8.GetBytes("teste"),
                imageUrl: "Article ImageUrl"
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe o nome!");
    }

    [Fact(DisplayName = "Não deve criar artigo quando o nome for nulo")]
    public void CreateArticle_WithNameValue_DomainExceptionInvalidName()
    {
        var action = () => 
            new Article
            (
                id: 1,
                name: null, 
                categoryId: 1, 
                userId: 1, 
                description: "Article Description", 
                content:  Encoding.UTF8.GetBytes("teste"),
                imageUrl: "Article ImageUrl"
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe o nome!");
    }

    [Fact(DisplayName = "Não deve criar artigo quando o nome for maior que 100 caracteres")]
    public void CreateArticle_WithNameInvalid_DomainExceptionInvalidName()
    {
        var name = "testeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee";

        var action = () => 
            new Article
            (
                id: 1,
                name: name, 
                categoryId: 1, 
                userId: 1, 
                description: "Article Description", 
                content:  Encoding.UTF8.GetBytes("teste"),
                imageUrl: "Article ImageUrl"
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Nome deve ser menor que 100 caracteres!");
    }
    #endregion

    #region CategoryId
    [Fact(DisplayName = "Não deve criar artigo quando o categoryId for negativo")]
    public void CreateArticle_NegativeCategoryIdValue_DomainExceptionInvalidId()
    {
        var action = () => 
            new Article
            (
                id: 1,
                name: "Article Name", 
                categoryId: -1, 
                userId: 1, 
                description: "Article Description", 
                content:  Encoding.UTF8.GetBytes("teste"),
                imageUrl: "Article ImageUrl"
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Inofrme a categoria!");
    }

    [Fact(DisplayName = "Não deve criar artigo quando o categoryId for zero")]
    public void CreateArticle_ZeroCategoryIdValue_DomainExceptionInvalidId()
    {
        var action = () => 
            new Article
            (
                id: 1,
                name: "Article Name", 
                categoryId: 0, 
                userId: 1, 
                description: "Article Description", 
                content:  Encoding.UTF8.GetBytes("teste"),
                imageUrl: "Article ImageUrl"
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Inofrme a categoria!");
    }
    #endregion

    #region UserId
    [Fact(DisplayName = "Não deve criar artigo quando o userId for negativo")]
    public void CreateArticle_NegativeUserIdValue_DomainExceptionInvalidId()
    {
        var action = () => 
            new Article
            (
                id: 1,
                name: "Article Name", 
                categoryId: 1, 
                userId: -1, 
                description: "Article Description", 
                content:  Encoding.UTF8.GetBytes("teste"),
                imageUrl: "Article ImageUrl"
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe o autor!");
    }

    [Fact(DisplayName = "Não deve criar artigo quando o userId for zero")]
    public void CreateArticle_ZeroUserIdValue_DomainExceptionInvalidId()
    {
        var action = () => 
            new Article
            (
                id: 1,
                name: "Article Name", 
                categoryId: 1, 
                userId: 0, 
                description: "Article Description", 
                content:  Encoding.UTF8.GetBytes("teste"),
                imageUrl: "Article ImageUrl"
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe o autor!");
    }
    #endregion

    #region Description
    [Fact(DisplayName = "Não deve criar artigo quando a descrição for maior que 1000 caracteres")]
    public void CreateArticle_BigDescriptionInvalid_DomainExceptionInvalidName()
    {
        var description = 
            @"testeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
              eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
              eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
              aaaaaaaaaaaaaaaaaatesteeeeeeeeeeeeeeeeeeeeeeeeeee
              eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
              eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
              aaaaaaaaaaaaaaaaaatesteeeeeeeeeeeeeeeeeeeeeeeeeee
              eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
              eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
              aaaaaaaaaaaaaaaaaatesteeeeeeeeeeeeeeeeeeeeeeeeeee
              eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
              eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
              aaaaaaaaaaaaaaaaaatesteeeeeeeeeeeeeeeeeeeeeeeeeee
              eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
              eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
              aaaaaaaaaaeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee";

        var action = () => 
            new Article
            (
                id: 1,
                name: "Article Name", 
                categoryId: 1, 
                userId: 1, 
                description: description, 
                content:  Encoding.UTF8.GetBytes("teste"),
                imageUrl: "Article ImageUrl"
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Descrição deve ser menor que 1000 caracteres!");
    }
    #endregion

    #region Content
    [Fact(DisplayName = "Não deve criar artigo quando o conteúdo for vazio")]
    public void CreateArticle_MissingContentValue_DomainExceptionRequiredContent()
    {
        var action = () => 
            new Article
            (
                id: 1,
                name: "Article Name", 
                categoryId: 1, 
                userId: 1, 
                description: "Article Description", 
                content: Encoding.UTF8.GetBytes(""),
                imageUrl: "Article ImageUrl"
            );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Conteúdo não informado!");
    }
    #endregion
}