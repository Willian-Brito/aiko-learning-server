using AikoLearning.Core.Application.Chat;
using AikoLearning.Core.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace AikoLearning.Presentation.WebAPI.Hubs;

public class ChatHub : Hub<IChatHub>
{
    public async Task SendMessage(ChatMessage message)
    {
        await Clients.All.ReceivedMessage(message);
    }
}