using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Data;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Repositories;
using TaskManager.Core.Services;
using TaskManager.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Entity Framework Core - InMemory
builder.Services.AddDbContext<TaskManagerDbContext>(options =>
    options.UseInMemoryDatabase("TaskManagerDb"));

// Repository
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Task Manager API",
        Version = "v1",
        Description = "API REST para gerenciamento de tarefas."
    });
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();