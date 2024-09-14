
namespace AikoLearning.Core.Domain.Exceptions;
public class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message) { }
}