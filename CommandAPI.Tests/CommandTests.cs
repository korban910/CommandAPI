using CommandAPI.Models;
using Xunit;
namespace CommandAPI.Tests;

public class CommandTests
{
    [Fact]
    public void CanChangeHowTo()
    {
        // Arrange
        var testCommand = new Command
        {
            HowTo = "Do Something awesome",
            Platform = "xUnit",
            CommandLine = "dotnet test"
        };

        var message = "Execute Unit Tests";
        
        // Act
        testCommand.HowTo = message;
        
        // Assert
        Assert.Equal(message, testCommand.HowTo);
    }

    [Fact]
    public void CanChangePlatform()
    {
        // Arrange
        var testCommand = new Command
        {
            HowTo = "Do Something awesome",
            Platform = "Windows",
            CommandLine = "dotnet test"
        };
        var platform = "xUnit";
        
        // Act
        testCommand.Platform = platform;
        
        // Assert
        Assert.Equal(platform, testCommand.Platform);
    }

    [Fact]
    public void CanChangeCommandLine()
    {
        // Arrange
        var testCommand = new Command
        {
            HowTo = "Do Something awesome",
            Platform = "xUnit",
            CommandLine = "Junco"
        };
        var commandLine = "dotnet test";
        
        // Act
        testCommand.CommandLine = commandLine;
        
        // Assert
        Assert.Equal(commandLine, testCommand.CommandLine);
    }
}