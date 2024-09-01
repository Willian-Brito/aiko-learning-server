using System.Text;
using AikoLearning.Infrastructure.Security.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AikoLearning.Infrastructure.IoC;

public static class DependencyInjectionJWT
{
    public static IServiceCollection AddInfrastructureJWT(this IServiceCollection services)
    {
        // informar o tipo de autenticação JWT-Bearer
        // definir o modelo de desafio de autenticação
        // este serviço irá extrair o token do cabeçalho da requisição
        services.AddAuthentication(opt => 
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
        // habilita a autenticação JWT usando o esquema de desafios definidos
        // validar token
        .AddJwtBearer(options => 
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                // Definindo o que será validado
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                
                // Informando valores para serem validados
                ValidIssuer = Settings.ISSUER,
                ValidAudience = Settings.AUDIENCE,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.SECRET_KEY)),
                
                // zerando a expiração para o serviço utilizar o que definimos no controller que será enviado no cabeçalho da requisição
                ClockSkew = TimeSpan.Zero 
            };
        });

        return services;
    }
}
