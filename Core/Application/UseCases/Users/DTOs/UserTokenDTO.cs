﻿namespace AikoLearning.Core.Application.DTOs;

public class UserTokenDTO
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
}
