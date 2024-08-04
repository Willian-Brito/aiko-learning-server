using AikoLearning.Infrastructure.IoC;

#region Services

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddInfrastructureJWT(builder.Configuration);
builder.Services.AddInfrastructureSwagger();

builder.Services.AddControllers();
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

app.ApplyMigrations();
app.AddInitialSeed();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion