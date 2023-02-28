using System;
using System.Collections.Generic;
using AutoMapper;
using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using CommandAPI.Profiles;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CommandAPI.Tests;

public class CommandsControllerTests : IDisposable
{
    private Mock<ICommandAPIRepo>? mockRepo;
    private CommandsProfile? realProfile;
    private MapperConfiguration? configuration;
    private IMapper? mapper;
    
    public CommandsControllerTests()
    {
        mockRepo = new Mock<ICommandAPIRepo>();
        realProfile = new CommandsProfile();
        configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
        mapper = new Mapper(configuration);
    }

    public void Dispose()
    {
        mockRepo = null;
        realProfile = null;
        configuration = null;
        realProfile = null;
    }

    [Fact]
    public void GetAllCommands_Returns200OK_WhenDBIsEmpty()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));
        var controller = new CommandsController(mockRepo.Object, mapper!);
        
        // Act
        var result = controller.GetAllCommands();
        
        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetAllCommands_ReturnsOneResource_WhenDBHasOneResource()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
        var controller = new CommandsController(mockRepo.Object, mapper!);
        
        // Act
        var result = controller.GetAllCommands();
        
        // Assert
        var okResult = result.Result as OkObjectResult;
        var commands = okResult!.Value as List<CommandReadDto>;
        Assert.Single(commands!);
    }

    [Fact]
    public void GetAllCommands_Returns200Ok_WhenDBHasOneResource()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
        var controller = new CommandsController(mockRepo.Object, mapper!);
        
        // Act
        var result = controller.GetAllCommands();
        
        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetAllCommands_ReturnCorrectObject_WhenDBHasOneResource()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
        var controller = new CommandsController(mockRepo.Object, mapper!);
        
        // Act
        var result = controller.GetAllCommands();
        
        // Assert
        Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
    }

    [Fact]
    public void GetCommandByID_Return404NotFound_WhenNonExistentIDProdivided()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
        var controller = new CommandsController(mockRepo.Object, mapper!);
        
        // Act
        var result = controller.GetCommandById(0);
        
        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void GetCommandsByID_Return200OK_WhenValidIDProvided()
    {
        //Arrange
        mockRepo!.Setup(repo => repo.GetCommandById(1)).Returns(new Command
        {
            HowTo = "mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });
        var controller = new CommandsController(mockRepo.Object, mapper!);
        
        // Act
        var result = controller.GetCommandById(1);
        
        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetCommandsByID_ReturnCorrectObject_WhenValidIDProvided()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetCommandById(2)).Returns(new Command
        {
            HowTo = "mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });
        var controller = new CommandsController(mockRepo.Object, mapper!);
        
        // Act
        var result = controller.GetCommandById(2);
        
        // Assert
        Assert.IsType<ActionResult<CommandReadDto>>(result);
    }

    [Fact]
    public void CreateCommand_ReturnCorrectObject_WhenValidObjectSubmitted()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetCommandById(1)).Returns(new Command
        {
            HowTo = "mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });
        var controller = new CommandsController(mockRepo.Object, mapper!);
        
        // Act
        var result = controller.CreateCommand(new CommandCreateUpdateDto());
        
        // Assert
        Assert.IsType<ActionResult<CommandReadDto>>(result);
    }

    [Fact]
    public void CreateCommand_Return201Created_WhenValidObjectSubmitted()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetCommandById(1)).Returns(new Command
        {
            HowTo = "mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });
        var controller = new CommandsController(mockRepo.Object, mapper!);
        
        // Act
        var result = controller.CreateCommand(new CommandCreateUpdateDto());
        
        // Assert
        Assert.IsType<CreatedAtRouteResult>(result.Result);
    }

    [Fact]
    public void UpdateCommand_Return204NoContent_WHenValidObjectSubmitted()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetCommandById(1)).Returns(new Command
        {
            HowTo = "mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });
        var controller = new CommandsController(mockRepo.Object, mapper!);
        
        // Act
        var result = controller.UpdateCommand(1, new CommandCreateUpdateDto());
        
        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void UpdateCommand_Return404NotFound_WhenNonExistentObjectSubmitted()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
        var controller = new CommandsController(mockRepo.Object, mapper!);
        
        // Act
        var result = controller.UpdateCommand(0, new CommandCreateUpdateDto());
        
        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void PartialCommandUpdate_Return404NotFound_WhenNonExistentObjectSubmitted()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
        var controller = new CommandsController(mockRepo.Object, mapper!);
        
        // Act
        var result = controller.PartialCommandUpdate(0, new JsonPatchDocument<CommandCreateUpdateDto>());
        
        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void DeleteCommand_Return204NoContent_WhenValidObjectSubmitted()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetCommandById(1)).Returns(new Command
        {
            HowTo = "mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });
        var controller = new CommandsController(mockRepo.Object, mapper!);
        
        // Act
        var result = controller.DeleteCommand(1);
        
        // Arrange
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeleteCommand_Return404NotFound_WhenNonExistentObjectSubmitted()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
        var controller = new CommandsController(mockRepo.Object, mapper!);
        
        // Act
        var result = controller.DeleteCommand(0);
        
        // Assert
        Assert.IsType<OkResult>(result);  //NotFoundResult -> for breaking test cases
    }

    private List<Command> GetCommands(int num)
    {
        var commands = new List<Command>();
        if (num > 0)
        {
            commands.Add(new Command
            {
                Id = 0,
                HowTo = "How to generate a migration",
                CommandLine = "dotnet ef migrations add <Name of Migration>",
                Platform = ".Net Core EF"
            });
        }

        return commands;
    }
}