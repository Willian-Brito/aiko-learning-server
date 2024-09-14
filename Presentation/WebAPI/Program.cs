using AikoLearning.Infrastructure.IoC;
using AikoLearning.Presentation.Middlewares;
using AikoLearning.Presentation.WebAPI.Conventions;
using AikoLearning.Presentation.WebAPI.Extencions.Converters;
using Newtonsoft.Json;

#region Services

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddInfrastructureJWT();
builder.Services.AddInfrastructureSwagger();

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

app.UseHttpsRedirection();
app.UseStatusCodePages();
app.UseGlobalErrorHandler();

app.ApplyMigrations();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion