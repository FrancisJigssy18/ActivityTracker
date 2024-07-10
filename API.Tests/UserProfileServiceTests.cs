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

public class UserProfileServiceTests
{
    private readonly UserProfileService _userProfileService;
    private readonly Mock<ActivityTrackerContext> _contextMock = new Mock<ActivityTrackerContext>();

    public UserProfileServiceTests()
    {
        _userProfileService = new UserProfileService(_contextMock.Object);
    }

    /// <summary>
    /// The test method `CreateUser_ShouldReturnCreatedUser` verifies that the `CreateUserProfileAsync`
    /// method in the `UserProfileService` creates a user profile with the expected properties.
    /// </summary>
    [Fact]
    public async Task CreateUser_ShouldReturnCreatedUser()
    {
        // Arrange
        var userProfile = new UserProfile { Name = "Test User", Weight = 70, Height = 175, BirthDate = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc) };

        _contextMock.Setup(x => x.UserProfiles.Add(It.IsAny<UserProfile>())).Verifiable();
        _contextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _userProfileService.CreateUserProfileAsync(userProfile);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test User", result.Name);
    }

    /// <summary>
    /// The test method `GetUserById_ShouldReturnUserProfile` verifies that the `GetUserProfileByIdAsync`
    /// method in the `UserProfileService` retrieves a user profile with the expected properties.
    /// </summary>
    [Fact]
    public async Task GetUserById_ShouldReturnUserProfile()
    {        
        // Arrange
        var userProfile = new UserProfile { Id = 1, Name = "Test User", Weight = 70, Height = 175, BirthDate = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc) };

        _contextMock.Setup(x => x.UserProfiles.FindAsync(1)).ReturnsAsync(userProfile);

        // Act
        var result = await _userProfileService.GetUserProfileByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test User", result.Name);
    }

    /// <summary>
    /// The test method `DeleteUser_ShouldRemoveUser` verifies that the `DeleteUserProfileAsync`
    /// method in the `UserProfileService` deletes a user profile with the expected properties.
    /// </summary>
    [Fact]
    public async Task DeleteUser_ShouldRemoveUser()
    {
        // Arrange
        var userProfile = new UserProfile { Id = 1, Name = "Test User", Weight = 70, Height = 175, BirthDate = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc) };

        _contextMock.Setup(x => x.UserProfiles.FindAsync(1)).ReturnsAsync(userProfile);
        _contextMock.Setup(x => x.UserProfiles.Remove(It.IsAny<UserProfile>())).Verifiable();
        _contextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        await _userProfileService.DeleteUserProfileAsync(1);

        // Assert
        _contextMock.Verify(x => x.UserProfiles.Remove(It.IsAny<UserProfile>()), Times.Once);
        _contextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }
}