using System.Security.Claims;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Infrastructure.Security.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace AikoLearning.Infrastructure.Security.Sessions;

public static class SessionExtensions
{
    public static int GetCurrentUserId(this HttpContext httpContext)
    {        
        var userIdClaim = httpContext?.User?.FindFirst(Settings.USER_ID_KEY);
        int.TryParse(userIdClaim.Value, out var userId);

        return userId;
    }

    public static IEnumerable<string> GetUserRoles(this HttpContext httpContext)
    {
        return httpContext?.User?.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value);
    }

    // public static void SetSession(HttpContext httpContext, UserSession userSession)
    // {
    //     httpContext.Response.Cookies.Append(
    //         Settings.SESSION_TOKEN_COOKIE,
    //         userSession.Key,
    //         new CookieOptions { Path = "/", SameSite = SameSiteMode.Lax }
    //     );
    //     httpContext.Response.Cookies.Append(
    //         CookieRequestCultureProvider.DefaultCookieName,
    //         CookieRequestCultureProvider.MakeCookieValue(
    //             new RequestCulture(userSession.User.Locale)
    //         ),
    //         new CookieOptions { SameSite = SameSiteMode.Lax }
    //     );
    // }
}
