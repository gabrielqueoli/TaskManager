using TaskManager.Core.Enums;

namespace TaskManager.Core.Entities;

public class Task
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public TaskStatusEnum Status { get; set; }

    public DateTime CreatedAt { get; set; }
}