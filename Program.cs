using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectef;
using projectef.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TasksContext>(p => p.UseInMemoryDatabase("TasksDB"));
builder.Services.AddNpgsql<TasksContext>(builder.Configuration.GetConnectionString("taskConnection"));

var app = builder.Build();

//GET Requests

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconnection", async ([FromServices] TasksContext dbContext) => 
{
    dbContext.Database.EnsureCreated();
    return Results.Ok($"Database in memory: {dbContext.Database.IsInMemory()}");
});

app.MapGet("/api/task", async ([FromServices] TasksContext dbContext)=>
{
    return Results.Ok(dbContext.tasks.Include(p=> p.Category));
});

app.MapGet("/api/categories", async ([FromServices] TasksContext dbContext)=>
{
    return Results.Ok(dbContext.categories);
});


// POST Requests

app.MapPost("/api/task", async ([FromServices] TasksContext dbContext, [FromBody] projectef.Models.Task task)=>
{
    task.TaskId = Guid.NewGuid();
    task.CreationDate = DateTime.UtcNow;
    await dbContext.AddAsync(task);

    await dbContext.SaveChangesAsync();

    return Results.Ok($"{task.Title} was created successfully");
});

app.MapPost("/api/category", async ([FromServices] TasksContext dbContext, [FromBody] Category category)=>
{
    category.CategoryId = Guid.NewGuid();

    await dbContext.AddAsync(category);
    await dbContext.SaveChangesAsync();

    return Results.Ok($"{category.Name} was created successfully");
});

//PUT Requests

app.MapPut("/api/task/{id}", async ([FromServices] TasksContext dbContext, [FromBody] projectef.Models.Task task, [FromRoute] Guid id)=>
{
    var currentTask = dbContext.tasks.Find(id);

    if(currentTask!=null)
    {
        currentTask.CategoryId = task.CategoryId;
        currentTask.Title = task.Title;
        currentTask.TaskPriority = task.TaskPriority;
        currentTask.Description = task.Description;

        await dbContext.SaveChangesAsync();

        return Results.Ok($"{task.Title} was updated successfully");
    }

    return Results.NotFound();
});

app.MapPut("/api/category/{id}", async ([FromServices] TasksContext dbContext, [FromBody] Category category, [FromRoute] Guid id)=>
{
    var currentCategory = dbContext.categories.Find(id);

    if(currentCategory!=null)
    {
        currentCategory.Name = category.Name;
        currentCategory.Description = category.Description;
        currentCategory.Weight = category.Weight;

        await dbContext.SaveChangesAsync();

        return Results.Ok($"{category.Name} was updated successfully");
    }

    return Results.NotFound();
});

//DELETE Requests

app.MapDelete("/api/tasks/{id}", async ([FromServices] TasksContext dbContext, [FromRoute] Guid id)=>
{
    var currentTask = dbContext.tasks.Find(id);

    if (currentTask!=null)
    {
        dbContext.Remove(currentTask);
        await dbContext.SaveChangesAsync();

        return Results.Ok("Task was deleted successfully");
    }

    return Results.NotFound();
});

app.MapDelete("/api/category/{id}", async ([FromServices] TasksContext dbContext, [FromRoute] Guid id)=>
{
    var currentCategory = dbContext.categories.Find(id);

    if (currentCategory!=null)
    {
        dbContext.Remove(currentCategory);
        await dbContext.SaveChangesAsync();

        return Results.Ok("Category was deleted successfully");
    }

    return Results.NotFound();
});

app.Run();