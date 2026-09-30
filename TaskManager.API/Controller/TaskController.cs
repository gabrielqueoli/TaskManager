using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.DTOs;
using TaskManager.Core.Enums;
using TaskManager.Core.Interfaces;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Create(CreateTaskDto dto)
    {
        var task = _service.Create(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = task.Id },
            task);
    }

    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] TaskStatusEnum? status,
        [FromQuery] DateTime? dueDate)
    {
        var tasks = _service.GetAll(status, dueDate);

        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var task = _service.GetById(id);

        if (task is null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, UpdateTaskDto dto)
    {
        var task = _service.Update(id, dto);

        if (task is null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var deleted = _service.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}