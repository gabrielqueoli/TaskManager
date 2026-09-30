using TaskManager.Core.DTOs;
using TaskManager.Core.Enums;
using TaskManager.Core.Interfaces;
using TaskEntity = TaskManager.Core.Entities.Task;

namespace TaskManager.Core.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public TaskResponseDto Create(CreateTaskDto dto)
    {
        if (!Enum.IsDefined(typeof(TaskStatusEnum), dto.Status))
        {
            throw new ArgumentException("Status inválido.");
        }
        var task = new TaskEntity
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow
        };

        var createdTask = _repository.Create(task);

        return MapToResponseDto(createdTask);
    }

    public TaskResponseDto? GetById(Guid id)
    {
        var task = _repository.GetById(id);

        if (task is null)
        {
            return null;
        }

        return MapToResponseDto(task);
    }

    public IEnumerable<TaskResponseDto> GetAll(
        TaskStatusEnum? status,
        DateTime? dueDate)
    {
        var tasks = _repository.GetAll();

        if (status.HasValue)
        {
            tasks = tasks.Where(task => task.Status == status.Value);
        }

        if (dueDate.HasValue)
        {
            tasks = tasks.Where(task =>
                task.DueDate.HasValue &&
                task.DueDate.Value.Date == dueDate.Value.Date);
        }

        return tasks.Select(MapToResponseDto);
    }

    public TaskResponseDto? Update(Guid id, UpdateTaskDto dto)
    {
        if (!Enum.IsDefined(typeof(TaskStatusEnum), dto.Status))
        {
            throw new ArgumentException("Status inválido.");
        }

        var task = _repository.GetById(id);

        if (task is null)
        {
            return null;
        }

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.DueDate = dto.DueDate;
        task.Status = dto.Status;

        _repository.Update(task);

        return MapToResponseDto(task);
    }

    public bool Delete(Guid id)
    {
        var task = _repository.GetById(id);

        if (task is null)
        {
            return false;
        }

        _repository.Delete(task);

        return true;
    }

    private static TaskResponseDto MapToResponseDto(TaskEntity task)
    {
        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            Status = task.Status,
            CreatedAt = task.CreatedAt
        };
    }
}