using System.Security.Claims;
using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Entities;

namespace AikoLearning.Core.Application.Auth.Interfaces;

public interface ITokenService
{
    Task<UserTokenDTO> Generate(User user, IEnumerable<Claim>? claims = null);
    ClaimsPrincipal GetClaimsFromExpiredToken(string token);
    Task<bool> IsTokenExpired(string token);
}