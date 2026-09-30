using TaskEntity = TaskManager.Core.Entities.Task;

namespace TaskManager.Core.Interfaces;

public interface ITaskRepository
{
    TaskEntity Create(TaskEntity task);

    TaskEntity? GetById(Guid id);

    IEnumerable<TaskEntity> GetAll();

    void Update(TaskEntity task);

    void Delete(TaskEntity task);
}