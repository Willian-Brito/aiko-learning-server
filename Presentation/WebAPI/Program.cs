using System.Threading.RateLimiting;
using AikoLearning.Infrastructure.IoC;
using AikoLearning.Presentation.Middlewares;
using AikoLearning.Presentation.WebAPI.Conventions;
using AikoLearning.Presentation.WebAPI.Extencions.Converters;
using AikoLearning.Presentation.WebAPI.Hubs;
using Microsoft.AspNetCore.RateLimiting;
using Newtonsoft.Json;

#region Services

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:5066");
builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddInfrastructureJWT();
builder.Services.AddInfrastructureSwagger();
builder.Services.AddSignalR();

builder.Services
    .AddControllers(options =>
    {
        options.Conventions.Add(new LowercaseRouteConvention());
    })
    .AddNewtonsoftJson(options =>
    {        
        options.SerializerSettings.Converters.Add(new ByteArrayJsonConverter());
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.Formatting = Formatting.Indented;
    });

// Rate Limiting
builder.Services.AddRateLimiter(options => 
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    #region Global
    // Global
    // options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext => 
    // {
    //     var partitionKey = httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString();
    //     Console.WriteLine($"Request from: {partitionKey}");

    //     return RateLimitPartition.GetFixedWindowLimiter(
    //         partitionKey: partitionKey,
    //         factory: partition => new FixedWindowRateLimiterOptions
    //         {
    //             AutoReplenishment = true,
    //             PermitLimit = 10,
    //             QueueLimit = 0,
    //             Window = TimeSpan.FromMinutes(1)
    //         }
    //     );
    // });
    #endregion

    #region Grupos

    #region API

    options.AddFixedWindowLimiter(policyName: "Api", options => 
    {
        options.PermitLimit = 100;
        options.Window = TimeSpan.FromMinutes(1);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 0;
    });
    #endregion

    #region Auth
    options.AddFixedWindowLimiter(policyName: "Auth", options => 
    {        
        options.PermitLimit = 10;
        options.Window = TimeSpan.FromMinutes(1);        
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 0;
    });
    #endregion

    #endregion

    #region Custom Message
    options.OnRejected = async (context, cancellationToken) => 
    {        
        Console.WriteLine($"{context.HttpContext.User.Identity?.Name} - Rate limit exceeded");

        context.HttpContext.Response.StatusCode = 429;
        await context.HttpContext.Response.WriteAsJsonAsync(
            new { Message = "Você excedeu o limite de requisições. Tente novamente mais tarde" }
        );
    };
    #endregion
});

// Mobile
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region App

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Chat
app.UseCors(cors => 
{
    cors.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:8081");        
});

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseStatusCodePages();
app.UseGlobalErrorHandler();
app.UseJobSchedule();

app.ApplyMigrations();

app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

app.MapControllers();
app.MapHub<ChatHub>("/api/chat");

app.Run();
#endregion