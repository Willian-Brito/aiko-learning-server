using AikoLearning.Infrastructure.IoC;
using AikoLearning.Presentation.Middlewares;
using AikoLearning.Presentation.WebAPI.Conventions;
using AikoLearning.Presentation.WebAPI.Extencions.Converters;
using AikoLearning.Presentation.WebAPI.Hubs;
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

// Mobile
builder.Services
    .AddCors(options =>
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
        .WithOrigins("http://localhost:8080");
});

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseStatusCodePages();
app.UseGlobalErrorHandler();
app.UseJobSchedule();

app.ApplyMigrations();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/api/chat");

app.Run();

#endregion