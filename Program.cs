using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TasksManagerApi.Models;
using TasksManagerApi.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDBSettings>(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING") ?? builder.Configuration.GetValue<string>("MongoDBSettings:ConnectionString")!;
    options.DatabaseName = Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME") ?? builder.Configuration.GetValue<string>("MongoDBSettings:DatabaseName")!;
});

builder.Services.AddSingleton<MongoDBContext>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoDBContext(settings);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Priorities

app.MapGet("api/v1/priorities", async (MongoDBContext _context) =>
{
    return await _context.Priorities.Find(_ => true).ToListAsync();
}).WithName("GetAllPriorities").WithOpenApi();

app.MapGet("api/v1/priorities/{id}", async (MongoDBContext _context, string id) =>
{
    return await _context.Priorities.Find(p => p.Id == id).FirstOrDefaultAsync();
}).WithName("GetPriorityById").WithOpenApi();

app.MapPost("api/v1/priorities", async (MongoDBContext _context, Priority priority) =>
{
    await _context.Priorities.InsertOneAsync(priority);
    return Results.Created($"api/v1/priorities/{priority.Id}", priority);
}).WithName("CreatePriority").WithOpenApi();

// Users

app.MapGet("api/v1/users", async (MongoDBContext _context) =>
{
    return await _context.Users.Find(_ => true).ToListAsync();
}).WithName("GetAllUsers").WithOpenApi();

app.MapGet("api/v1/users/enable", async(MongoDBContext _context) =>
{
    return await _context.Users.Find(u => u.Status == 1).ToListAsync();
}).WithName("GetOnlyEnableUsers").WithOpenApi();

app.MapGet("api/v1/users/disable", async (MongoDBContext _context) =>
{
    return await _context.Users.Find(u => u.Status == 2).ToListAsync();
}).WithName("GetOnlyDisableUsers").WithOpenApi();

app.MapGet("api/v1/users/{id}", async (MongoDBContext _context, string id) =>
{
    return await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
}).WithName("GetUserById").WithOpenApi();

app.MapPost("api/v1/users", async (MongoDBContext _context, User user) =>
{
    await _context.Users.InsertOneAsync(user);
    return Results.Created($"api/v1/users/{user.Id}", user);
}).WithName("CreateUser").WithOpenApi();

// Tasks

app.MapGet("api/v1/tasks", async (MongoDBContext _context) =>
{
    return await _context.Tasks.Find(_ => true).ToListAsync();
}).WithName("GetAllTasks").WithOpenApi();

app.MapGet("api/v1/tasks/enable", async (MongoDBContext _context) =>
{
    return await _context.Tasks.Find(t => t.Status == 1).ToListAsync();
}).WithName("GetTasksEnable").WithOpenApi();

app.MapGet("api/v1/tasks/disable", async (MongoDBContext _context) =>
{
    return await _context.Tasks.Find(t => t.Status == 2).ToListAsync();
}).WithName("GetTasksDisable").WithOpenApi();

app.MapGet("api/v1/tasks/completed", async (MongoDBContext _context) =>
{
    return await _context.Tasks.Find(t => t.IsCompleted == true).ToListAsync();
}).WithName("GetTasksCompleted").WithOpenApi();

app.MapGet("api/v1/tasks/incompleted", async (MongoDBContext _context) =>
{
    return await _context.Tasks.Find(t => !t.IsCompleted).ToListAsync();
}).WithName("GetTasksIncompleted").WithOpenApi();

app.MapGet("api/v1/tasks/{id:int}", async(MongoDBContext _context, string id) =>
{
    return await _context.Tasks.Find(t => t.Id == id).FirstOrDefaultAsync();
});

app.MapPost("api/v1/tasks", async (MongoDBContext _context, TasksManagerApi.Models.Task task) =>
{
    await _context.Tasks.InsertOneAsync(task);
    return Results.Created($"api/v1/tasks/{task.Id}", task);
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
