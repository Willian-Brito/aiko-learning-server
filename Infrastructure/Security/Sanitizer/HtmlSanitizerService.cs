using System.Text;
using System.Web;
using Ganss.Xss;

namespace AikoLearning.Infrastructure.Security.Sanitizer;

public class HtmlSanitizerService : Core.Application.Interfaces.IHtmlSanitizer
{
    public byte[] Sanitize(byte[] content)
    {
        var sanitizer = new HtmlSanitizer();
        var htmlContent = Encoding.UTF8.GetString(content);            
        var sanitizedHtml = sanitizer.Sanitize(htmlContent);            
        var sanitizedBytes = Encoding.UTF8.GetBytes(sanitizedHtml);

        return sanitizedBytes;
    }

    public string Sanitize(string content)
    {
        var sanitizer = new HtmlSanitizer();          
        var sanitized = sanitizer.Sanitize(content);

        return sanitized;
    }

    public byte[] Encode(byte[] content)
    {
        var encoded = HttpUtility.HtmlEncode(content);
        var encodedBytes = Encoding.UTF8.GetBytes(encoded);
        return encodedBytes;
    }

    public string Encode(string html)
    {
        var encoded = HttpUtility.HtmlEncode(html);
        return encoded;
    }
}