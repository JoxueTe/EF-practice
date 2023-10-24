using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectef;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TasksContext>(p => p.UseInMemoryDatabase("TasksDB"));
builder.Services.AddNpgsql<TasksContext>(builder.Configuration.GetConnectionString("taskConnection"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconnection", async ([FromServices] TasksContext dbContext) => 
{
    dbContext.Database.EnsureCreated();
    return Results.Ok($"Database in memory: {dbContext.Database.IsInMemory()}");
});

app.MapGet("/api/tareas", async ([FromServices] TasksContext dbContext)=>
{
    return Results.Ok(dbContext.tasks.Include(p=> p.Category));
});

app.MapPost("/api/tareas", async ([FromServices] TasksContext dbContext, [FromBody] projectef.Models.Task task)=>
{
    task.TaskId = Guid.NewGuid();
    task.CreationDate = DateTime.UtcNow;
    await dbContext.AddAsync(task);

    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

app.Run();