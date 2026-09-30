using TaskManager.Core.Enums;

namespace TaskManager.Core.DTOs;

public class TaskResponseDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public TaskStatusEnum Status { get; set; }

    public DateTime CreatedAt { get; set; }
}