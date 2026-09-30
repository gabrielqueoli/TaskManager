using Microsoft.EntityFrameworkCore;
using TaskEntity = TaskManager.Core.Entities.Task;

namespace TaskManager.Core.Data;

public class TaskManagerDbContext : DbContext
{
    public TaskManagerDbContext(
        DbContextOptions<TaskManagerDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
}