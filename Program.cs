using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectef;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TasksContext>(p => p.UseInMemoryDatabase("TasksDB"));
builder.Services.AddNpgsql<TasksContext>("User ID=postgres;Password=postpassword;Host=localhost;Port=5432;Database=TaskDB;");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconnection", async ([FromServices] TasksContext dbContext) => 
{
    dbContext.Database.EnsureCreated();
    return Results.Ok($"Database in memory: {dbContext.Database.IsInMemory()}");
});

app.Run();