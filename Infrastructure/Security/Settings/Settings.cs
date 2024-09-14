namespace AikoLearning.Infrastructure.Security.Configuration;

public static class Settings
{
    public static readonly string SECRET_KEY = "%abra#cadabra$sim@salabim*2023|%abra#cadabra$sim@salabim*2023|123456789";
    public static readonly string ISSUER = "aiko.digital";
    public static readonly string AUDIENCE = "http://aiko.digital/";
    public static readonly string USER_ID_KEY = "UserId";
    public static readonly string EMAIL_KEY = "EmailKey";
    public static string SESSION_TOKEN_COOKIE = "authorization-token";
    // public static readonly int ACCESS_TOKEN_EXPIRATION = 3600; // 1 hora
    // public static readonly int REFRESH_TOKEN_EXPIRATION = 7200; // 2 horas
    public static readonly int ACCESS_TOKEN_EXPIRATION = 86400; // 1 dia
    public static readonly int REFRESH_TOKEN_EXPIRATION = 86400;
}