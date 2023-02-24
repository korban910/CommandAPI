using System.Collections.Generic;
using AutoMapper;
using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.Models;
using CommandAPI.Profiles;
using Moq;
using Xunit;

namespace CommandAPI.Tests;

public class CommandsControllerTests
{
    [Fact]
    public void GetAllCommands_ReturnsZeroItems_WhenDBIsEmpty()
    {
        // Arrange
        // We need to create an instance of our CommandsController class
        //var controller = new CommandsController( /* repository, AutoMapper */);
    }

    [Fact]
    public void GetAllCommands_Returns200OK_WhenDBIsEmpty()
    {
        // Arrange
        var mockRepo = new Mock<ICommandAPIRepo>();
        mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));
        var realProfile = new CommandsProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
        var mapper = new Mapper(configuration);
        var controller = new CommandsController(mockRepo.Object, mapper);
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