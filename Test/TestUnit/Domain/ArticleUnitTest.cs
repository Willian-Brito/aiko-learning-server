using System.Text;
using AikoLearning.Core.Application.Interfaces;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Exceptions;
using FluentAssertions;
using Moq;

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

    [Fact(DisplayName = "Conteúdo do artigo não deve ter caracteres maliciosos quando for string")]
    public void ArticleContent_ShouldNotHave_MaliciousCharactersString()
    {        
        var mockHtmlSanitizer = new Mock<IHtmlSanitizer>();

        var html = @"<script>alert('xss')</script><div onload=""alert('xss')"""
            + @"style=""background-color: rgba(0, 0, 0, 1)"">Test<img src=""test.png"""
            + @"style=""background-image: url(javascript:alert('xss')); margin: 10px""></div>";

        var expected = @"<div style=""background-color: rgba(0, 0, 0, 1)"">"
            + @"Test<img src=""test.png"" style=""margin: 10px""></div>";

        mockHtmlSanitizer.Setup(s => s.Sanitize(html)).Returns(expected);

        var sanitized = mockHtmlSanitizer.Object.Sanitize(html);
        
        Assert.Equal(expected, sanitized);        
        mockHtmlSanitizer.Verify(s => s.Sanitize(html), Times.Once);
    }

    [Fact(DisplayName = "Encode deve codificar caracteres HTML quando for string")]
    public void Encode_ShouldEncodeHtmlCharactersString()
    {
        var mockHtmlSanitizer = new Mock<IHtmlSanitizer>();

        var html = "<div>Test & Encode</div>";
        var expected = "&lt;div&gt;Test &amp; Encode&lt;/div&gt;";

        mockHtmlSanitizer.Setup(s => s.Encode(html)).Returns(expected);
        var encoder = mockHtmlSanitizer.Object.Encode(html);
        
        Assert.Equal(expected, encoder);
        mockHtmlSanitizer.Verify(s => s.Encode(html), Times.Once);
    }

    [Fact(DisplayName = "Conteúdo do artigo não deve ter caracteres maliciosos quando for um array de bytes")]
    public void ArticleContent_ShouldNotHave_MaliciousCharactersByteArray()
    {        
        var mockHtmlSanitizer = new Mock<IHtmlSanitizer>();

        var html = @"<script>alert('xss')</script><div onload=""alert('xss')"""
            + @"style=""background-color: rgba(0, 0, 0, 1)"">Test<img src=""test.png"""
            + @"style=""background-image: url(javascript:alert('xss')); margin: 10px""></div>";

        var expected = @"<div style=""background-color: rgba(0, 0, 0, 1)"">"
            + @"Test<img src=""test.png"" style=""margin: 10px""></div>";

        var htmlBytes = Encoding.UTF8.GetBytes(html);
        var expectedBytes = Encoding.UTF8.GetBytes(expected);

        mockHtmlSanitizer.Setup(s => s.Sanitize(htmlBytes)).Returns(expectedBytes);

        var sanitized = mockHtmlSanitizer.Object.Sanitize(htmlBytes);
        
        Assert.Equal(expectedBytes, sanitized);        
        mockHtmlSanitizer.Verify(s => s.Sanitize(htmlBytes), Times.Once);
    }

    [Fact(DisplayName = "Encode deve codificar caracteres HTML quando for um array de bytes")]
    public void Encode_ShouldEncodeHtmlCharactersByteArray()
    {
        var mockHtmlSanitizer = new Mock<IHtmlSanitizer>();

        var html = "<div>Test & Encode</div>";
        var expected = "&lt;div&gt;Test &amp; Encode&lt;/div&gt;";
        var htmlBytes = Encoding.UTF8.GetBytes(html);
        var expectedBytes = Encoding.UTF8.GetBytes(expected);

        mockHtmlSanitizer.Setup(s => s.Encode(htmlBytes)).Returns(expectedBytes);
        var encoder = mockHtmlSanitizer.Object.Encode(htmlBytes);
        
        Assert.Equal(expectedBytes, encoder);
        mockHtmlSanitizer.Verify(s => s.Encode(htmlBytes), Times.Once);
    }
    #endregion
}