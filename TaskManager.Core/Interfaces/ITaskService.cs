using TaskManager.Core.DTOs;
using TaskManager.Core.Enums;

namespace TaskManager.Core.Interfaces;

public interface ITaskService
{
    TaskResponseDto Create(CreateTaskDto dto);

    TaskResponseDto? GetById(Guid id);

    IEnumerable<TaskResponseDto> GetAll(
        TaskStatusEnum? status,
        DateTime? dueDate);

    TaskResponseDto? Update(Guid id, UpdateTaskDto dto);

    bool Delete(Guid id);
}