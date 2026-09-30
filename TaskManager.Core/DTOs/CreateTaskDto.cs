using System.ComponentModel.DataAnnotations;
using TaskManager.Core.Enums;

namespace TaskManager.Core.DTOs;

public class CreateTaskDto
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [MaxLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres.")]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Pendente;
}