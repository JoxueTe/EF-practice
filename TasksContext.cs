using Microsoft.EntityFrameworkCore;
using projectef.Models;

namespace projectef;

public class TasksContext: DbContext
{
    public DbSet<Category> categories {get; set;}
    public DbSet<Models.Task> tasks {get; set;}
    public TasksContext(DbContextOptions<TasksContext> options) : base(options){}
}