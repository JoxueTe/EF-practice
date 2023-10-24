using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using projectef.Models;

namespace projectef;

public class TasksContext: DbContext
{
    public DbSet<Category> categories {get; set;}
    public DbSet<Models.Task> tasks {get; set;}
    public TasksContext(DbContextOptions<TasksContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Category> categoriesInit = new List<Category>();
        categoriesInit.Add(new Category() { CategoryId = Guid.Parse("7bbef3f5-d64f-41ec-b201-38fb50cb9d5d"),Name="Physical Activity", Weight = 7});
        categoriesInit.Add(new Category() { CategoryId = Guid.Parse("7bbef3f5-d64f-41ec-b201-38fb50cb9d56"),Name="Study", Weight = 10});


        modelBuilder.Entity<Category>(category =>
        {
            category.ToTable("Category");
            category.HasKey(p => p.CategoryId);

            category.Property(p=> p.Name).IsRequired().HasMaxLength(150);

            category.Property(p=> p.Description).IsRequired(false);

            category.Property(p=> p.Weight);

            category.HasData(categoriesInit);
        });

        List<Models.Task> taskInit = new List<Models.Task>();
        taskInit.Add(new Models.Task() {TaskId=Guid.Parse("7bbef3f5-d64f-41ec-b201-38fb50cb9d10"), CategoryId=Guid.Parse("7bbef3f5-d64f-41ec-b201-38fb50cb9d5d"), TaskPriority= Priority.medium, Title="Excercise", CreationDate=DateTime.UtcNow});
        taskInit.Add(new Models.Task() {TaskId=Guid.Parse("7bbef3f5-d64f-41ec-b201-38fb50cb9d11"), CategoryId=Guid.Parse("7bbef3f5-d64f-41ec-b201-38fb50cb9d56"), TaskPriority= Priority.high, Title="Blazor Course", CreationDate=DateTime.UtcNow});

        modelBuilder.Entity<Models.Task>(task =>
        {
            task.ToTable("Task");
            task.HasKey(p => p.TaskId);

            task.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey(p => p.CategoryId);

            task.Property(p => p.Title).IsRequired().HasMaxLength(200);

            task.Property(p => p.Description).IsRequired(false);

            task.Property(p => p.TaskPriority);
            
            task.Property(p => p.CreationDate);

            task.Ignore(p => p.Summary);

            task.HasData(taskInit);
        });
    }
}