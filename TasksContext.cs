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
        modelBuilder.Entity<Category>(category =>
        {
            category.ToTable("Category");
            category.HasKey(p => p.CategoryId);

            category.Property(p=> p.Name).IsRequired().HasMaxLength(150);

            category.Property(p=> p.Description);
        });

        modelBuilder.Entity<Models.Task>(task =>
        {
            task.ToTable("Task");
            task.HasKey(p => p.TaskId);

            task.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey(p => p.CategoryId);
            task.Property(p => p.Category);

            task.Property(p => p.Title).IsRequired().HasMaxLength(200);

            task.Property(p => p.Description);

            task.Property(p => p.TaskPriority);
            
            task.Property(p => p.CreationDate);

            task.Ignore(p => p.Summary);
        });
    }
}