using TaskManager.Core.DTOs;
using TaskManager.Core.Enums;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Services;
using TaskEntity = TaskManager.Core.Entities.Task;

namespace TaskManager.Tests;

public class TaskServiceTests
{
    [Fact]
    public void Create_ShouldCreateTaskSuccessfully()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService(repository);

        var dto = new CreateTaskDto
        {
            Title = "Estudar C#",
            Description = "Estudar testes automatizados",
            Status = TaskStatusEnum.Pendente
        };

        // Act
        var result = service.Create(dto);

        // Assert
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal("Estudar C#", result.Title);
        Assert.Equal(TaskStatusEnum.Pendente, result.Status);
    }

    [Fact]
    public void GetById_ShouldReturnTask_WhenTaskExists()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService(repository);

        var createdTask = service.Create(new CreateTaskDto
        {
            Title = "Tarefa de teste",
            Status = TaskStatusEnum.Pendente
        });

        // Act
        var result = service.GetById(createdTask.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createdTask.Id, result.Id);
        Assert.Equal("Tarefa de teste", result.Title);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenTaskDoesNotExist()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService(repository);

        // Act
        var result = service.GetById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAll_ShouldFilterByStatus()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService(repository);

        service.Create(new CreateTaskDto
        {
            Title = "Tarefa pendente",
            Status = TaskStatusEnum.Pendente
        });

        service.Create(new CreateTaskDto
        {
            Title = "Tarefa concluída",
            Status = TaskStatusEnum.Concluida
        });

        // Act
        var result = service.GetAll(
            TaskStatusEnum.Pendente,
            null);

        // Assert
        Assert.Single(result);
        Assert.Equal(
            "Tarefa pendente",
            result.First().Title);
    }

    [Fact]
    public void GetAll_ShouldFilterByDueDate()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService(repository);

        service.Create(new CreateTaskDto
        {
            Title = "Tarefa de hoje",
            DueDate = new DateTime(2026, 10, 5, 18, 0, 0),
            Status = TaskStatusEnum.Pendente
        });

        service.Create(new CreateTaskDto
        {
            Title = "Tarefa de amanhã",
            DueDate = new DateTime(2026, 10, 6, 18, 0, 0),
            Status = TaskStatusEnum.Pendente
        });

        // Act
        var result = service.GetAll(
            null,
            new DateTime(2026, 10, 5));

        // Assert
        Assert.Single(result);
        Assert.Equal(
            "Tarefa de hoje",
            result.First().Title);
    }

    [Fact]
    public void Update_ShouldUpdateTaskSuccessfully()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService(repository);

        var createdTask = service.Create(new CreateTaskDto
        {
            Title = "Título original",
            Description = "Descrição original",
            Status = TaskStatusEnum.Pendente
        });

        var updateDto = new UpdateTaskDto
        {
            Title = "Título atualizado",
            Description = "Descrição atualizada",
            DueDate = new DateTime(2026, 10, 20),
            Status = TaskStatusEnum.EmProgresso
        };

        // Act
        var result = service.Update(
            createdTask.Id,
            updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Título atualizado", result.Title);
        Assert.Equal("Descrição atualizada", result.Description);
        Assert.Equal(new DateTime(2026, 10, 20), result.DueDate);
        Assert.Equal(TaskStatusEnum.EmProgresso, result.Status);
    }

    [Fact]
    public void Update_ShouldReturnNull_WhenTaskDoesNotExist()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService(repository);

        var updateDto = new UpdateTaskDto
        {
            Title = "Tarefa atualizada",
            Status = TaskStatusEnum.Concluida
        };

        // Act
        var result = service.Update(
            Guid.NewGuid(),
            updateDto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Delete_ShouldReturnTrue_WhenTaskExists()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService(repository);

        var createdTask = service.Create(new CreateTaskDto
        {
            Title = "Tarefa para excluir",
            Status = TaskStatusEnum.Pendente
        });

        // Act
        var result = service.Delete(createdTask.Id);

        // Assert
        Assert.True(result);
        Assert.Null(service.GetById(createdTask.Id));
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenTaskDoesNotExist()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService(repository);

        // Act
        var result = service.Delete(Guid.NewGuid());

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Create_ShouldThrowException_WhenStatusIsInvalid()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService(repository);

        var dto = new CreateTaskDto
        {
            Title = "Tarefa inválida",
            Status = (TaskStatusEnum)99
        };

        // Act + Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            service.Create(dto));

        Assert.Equal("Status inválido.", exception.Message);
    }

    [Fact]
    public void Update_ShouldThrowException_WhenStatusIsInvalid()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService(repository);

        var createdTask = service.Create(new CreateTaskDto
        {
            Title = "Tarefa original",
            Status = TaskStatusEnum.Pendente
        });

        var updateDto = new UpdateTaskDto
        {
            Title = "Tarefa atualizada",
            Status = (TaskStatusEnum)99
        };

        // Act + Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            service.Update(createdTask.Id, updateDto));

        Assert.Equal("Status inválido.", exception.Message);
    }
}