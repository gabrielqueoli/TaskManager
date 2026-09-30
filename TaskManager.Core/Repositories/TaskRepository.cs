using Microsoft.EntityFrameworkCore;
using TaskEntity = TaskManager.Core.Entities.Task;
using TaskManager.Core.Data;
using TaskManager.Core.Interfaces;

namespace TaskManager.Core.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskManagerDbContext _context;

    public TaskRepository(TaskManagerDbContext context)
    {
        _context = context;
    }

    public TaskEntity Create(TaskEntity task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();

        return task;
    }

    public TaskEntity? GetById(Guid id)
    {
        return _context.Tasks
            .FirstOrDefault(task => task.Id == id);
    }

    public IEnumerable<TaskEntity> GetAll()
    {
        return _context.Tasks
            .AsNoTracking()
            .ToList();
    }

    public void Update(TaskEntity task)
    {
        _context.Tasks.Update(task);
        _context.SaveChanges();
    }

    public void Delete(TaskEntity task)
    {
        _context.Tasks.Remove(task);
        _context.SaveChanges();
    }
}