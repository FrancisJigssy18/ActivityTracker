using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections;

namespace API.Tests;

public class RunningActivityServiceTests
{
    private readonly RunningActivityService _runningActivityService;
    private readonly Mock<ActivityTrackerContext> _contextMock = new Mock<ActivityTrackerContext>();

    public RunningActivityServiceTests()
    {
        _runningActivityService = new RunningActivityService(_contextMock.Object);
    }

    /// <summary>
    /// The test method `CreateRunningActivity_ShouldReturnCreatedRunningActivity` verifies the creation of
    /// a running activity with specific properties.
    /// </summary>
    [Fact]
    public async Task CreateRunningActivity_ShouldReturnCreatedRunningActivity()
    {
        // Arrange
        var runningActivity = new RunningActivity { Location = "Cebu City", Distance = 2.5, StartTime = new DateTime(2024, 7, 10, 03, 10, 0, DateTimeKind.Utc) };

        _contextMock.Setup(x => x.RunningActivities.Add(It.IsAny<RunningActivity>())).Verifiable();
        _contextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _runningActivityService.CreateRunningActivityAsync(runningActivity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Cebu City", result.Location);
    }

    /// <summary>
    /// The test method verifies that the GetRunningActivityByIdAsync method in a C# application returns a
    /// running activity with the correct location.
    /// </summary>
    [Fact]
    public async Task GetRunningActivityById_ShouldReturnRunningActivity()
    {
        // Arrange
        var runningActivity = new RunningActivity { Location = "Cebu City", Distance = 2.5, StartTime = new DateTime(2024, 7, 10, 03, 10, 0, DateTimeKind.Utc) };

        _contextMock.Setup(x => x.RunningActivities.FindAsync(1)).ReturnsAsync(runningActivity);

        // Act
        var result = await _runningActivityService.GetRunningActivityByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Cebu City", result.Location);
    }

    /// <summary>
    /// The DeleteRunningActivity_ShouldRemoveRunningActivity test method verifies that the running activity
    /// is removed from the context when deleted.
    /// </summary>
    [Fact]
    public async Task DeleteRunningActivity_ShouldRemoveRunningActivity()
    {
        // Arrange
        var runningActivity = new RunningActivity { Location = "Cebu City", Distance = 2.5, StartTime = new DateTime(2024, 7, 10, 03, 10, 0, DateTimeKind.Utc) };

        _contextMock.Setup(x => x.RunningActivities.FindAsync(1)).ReturnsAsync(runningActivity);
        _contextMock.Setup(x => x.RunningActivities.Remove(It.IsAny<RunningActivity>())).Verifiable();
        _contextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        await _runningActivityService.DeleteRunningActivityAsync(1);

        // Assert
        _contextMock.Verify(x => x.RunningActivities.Remove(It.IsAny<RunningActivity>()), Times.Once);
        _contextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }
}