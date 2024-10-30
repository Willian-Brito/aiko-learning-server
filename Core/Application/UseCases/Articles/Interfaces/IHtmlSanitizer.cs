namespace AikoLearning.Core.Application.Interfaces;

public interface IHtmlSanitizer
{
    byte[] Sanitize(byte[] content);
    string Sanitize(string content);
    string Encode(string html);
    byte[] Encode(byte[] content);
}
