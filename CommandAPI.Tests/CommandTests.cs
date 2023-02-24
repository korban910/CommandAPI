using System;
using CommandAPI.Models;
using Xunit;
namespace CommandAPI.Tests;

public class CommandTests : IDisposable
{
    private Command testCommand;

    public CommandTests()
    {
        testCommand = new Command
        {
            HowTo = "Do Something awesome",
            Platform = "xUnit",
            CommandLine = "dotnet test"
        };
    }

    public void Dispose()
    {
        testCommand = null;
    }
    
    [Fact]
    public void CanChangeHowTo()
    {
        // Arrange
        const string message = "Execute Unit Tests";
        
        // Act
        testCommand.HowTo = message;
        
        // Assert
        Assert.Equal(message, testCommand.HowTo);
    }

    [Fact]
    public void CanChangePlatform()
    {
        // Arrange
        const string platform = "xUnit";
        
        // Act
        testCommand.Platform = platform;
        
        // Assert
        Assert.Equal(platform, testCommand.Platform);
    }

    [Fact]
    public void CanChangeCommandLine()
    {
        // Arrange
        const string commandLine = "dotnet test";
        
        // Act
        testCommand.CommandLine = commandLine;
        
        // Assert
        Assert.Equal(commandLine, testCommand.CommandLine);
    }
}