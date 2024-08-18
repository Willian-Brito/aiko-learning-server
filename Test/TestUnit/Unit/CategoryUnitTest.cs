using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Validations;
using FluentAssertions;

namespace TestUnit.Unit;

public class CategoryUnitTest
{
    #region ID
    [Fact(DisplayName = "Não deve criar categoria quando o id for negativo")]
    public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
    {
        var action = () => new Category(-1, "Category Name", null);

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("id da categoria inválido!");
    }
    #endregion

    #region Category
    [Fact(DisplayName = "Criar categoria com o estado válido")]
    public void CreateCategory_WithValidParameters_ResultObjectValidState()
    {
        var action = () => new Category(1, "Category Name", null);
        action.Should().NotThrow<DomainValidationException>();
    }
    #endregion

    #region Name

    [Fact(DisplayName = "Não deve criar categoria quando o nome for menor que 3 caracteres")]
    public void CreateCategory_ShortNameValue_DomainExceptionRequiredName()
    {
        var action = () => new Category(1, "Ca", null);

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Nome inválido, é necessário ter no minimo 3 caracteres!");
    }

    [Fact(DisplayName = "Não deve criar categoria quando o nome for vazio")]
    public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
    {
        var action = () => new Category(1, "", null);

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe o nome!");
    }

    [Fact(DisplayName = "Não deve criar categoria quando o nome for nulo")]
    public void CreateCategory_WithNameValue_DomainExceptionInvalidName()
    {
        var action = () => new Category(1, null, null);

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe o nome!");
    }

    [Fact(DisplayName = "Não deve criar categoria quando o nome for maior que 100 caracteres")]
    public void CreateCategory_WithNameInvalid_DomainExceptionInvalidName()
    {
        var action = () => new Category(
            id: 1, 
            name: "testeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee", 
            parentId: null
        );

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Nome deve ser menor que 100 caracteres!");
    }
    #endregion
}