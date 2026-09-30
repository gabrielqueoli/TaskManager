using TaskEntity = TaskManager.Core.Entities.Task;
using TaskManager.Core.Interfaces;

namespace TaskManager.Tests;

public class FakeTaskRepository : ITaskRepository
{
    private readonly List<TaskEntity> _tasks = new();

    public TaskEntity Create(TaskEntity task)
    {
        _tasks.Add(task);
        return task;
    }

    public TaskEntity? GetById(Guid id)
    {
        return _tasks.FirstOrDefault(task => task.Id == id);
    }

    public IEnumerable<TaskEntity> GetAll()
    {
        return _tasks;
    }

    public void Update(TaskEntity task)
    {
        var existingTask = GetById(task.Id);

        if (existingTask is null)
        {
            return;
        }

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.DueDate = task.DueDate;
        existingTask.Status = task.Status;
    }

    public void Delete(TaskEntity task)
    {
        _tasks.Remove(task);
    }
}