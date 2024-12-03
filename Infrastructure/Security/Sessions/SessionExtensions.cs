using System.Security.Claims;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Infrastructure.Security.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace AikoLearning.Infrastructure.Security.Sessions;

public static class SessionExtensions
{
    public static int? GetCurrentUserId(this HttpContext httpContext)
    {        
        var userIdClaim = httpContext?.User?.FindFirst(Settings.USER_ID_KEY);

        if(userIdClaim is not null)
        {
            int.TryParse(userIdClaim.Value, out var userId);
            return userId;
        }

        return null;
    }

    public static IEnumerable<string> GetUserRoles(this HttpContext httpContext)
    {
        return httpContext?.User?.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value);
    }
}
