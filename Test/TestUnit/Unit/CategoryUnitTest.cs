using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Validations;
using FluentAssertions;

namespace TestUnit.Unit;

public class CategoryUnitTest
{
    [Fact(DisplayName = "Não deve criar categoria quando o id for negativo")]
    public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Category(-1, "Category Name", null);

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("id da categoria inválido!");
    }

    [Fact(DisplayName = "Criar categoria com o estado válido")]
    public void CreateCategory_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Category(1, "Category Name", null);
        action.Should().NotThrow<DomainValidationException>();
    }

    [Fact(DisplayName = "Não deve criar categoria quando o nome for menor que 3 caracteres")]
    public void CreateCategory_ShortNameValue_DomainExceptionRequiredName()
    {
        Action action = () => new Category(1, "Ca", null);

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Nome inválido, é necessário ter no minimo 3 caracteres!");
    }

    [Fact(DisplayName = "Não deve criar categoria quando o nome for vazio")]
    public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
    {
        Action action = () => new Category(1, "", null);

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe o nome!");
    }

    [Fact(DisplayName = "Não deve criar categoria quando o nome for nulo")]
    public void CreateCategory_WithNameValue_DomainExceptionInvalidName()
    {
        Action action = () => new Category(1, null, null);

        action.Should()
              .Throw<DomainValidationException>()
              .WithMessage("Informe o nome!");
    }
}