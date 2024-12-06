using AikoLearning.Core.Domain.Entities;

namespace AikoLearning.Core.Application.Chat;

public interface IChatHub
{
    Task ReceivedMessage(ChatMessage message);
}