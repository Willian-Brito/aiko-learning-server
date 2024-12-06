namespace AikoLearning.Core.Domain.Entities;

public class ChatMessage
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Body { get; private set; }
    public DateTime Date { get; private set; }

    public ChatMessage(string name, string email, string body)
    {
        Name = name;
        Email = email;
        Body = body;
        Date = DateTime.Now;
    }
}