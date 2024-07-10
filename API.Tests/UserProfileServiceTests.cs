using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests;

public class UserProfileServiceTests
{
    private readonly UserProfileService _userProfileService;
    private readonly Mock<ActivityTrackerContext> _contextMock = new Mock<ActivityTrackerContext>();

    public UserProfileServiceTests()
    {
        _userProfileService = new UserProfileService(_contextMock.Object);
    }

    [Fact]
    public async Task CreateUser_ShouldReturnCreatedUser()
    {
        var userProfile = new UserProfile { Name = "Test User", Weight = 70, Height = 175, BirthDate = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc) };
        
        _contextMock.Setup(x => x.UserProfiles.Add(It.IsAny<UserProfile>())).Verifiable();
        _contextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _userProfileService.CreateUserProfileAsync(userProfile);
        
        Assert.NotNull(result);
        Assert.Equal("Test User", result.Name);
    }
}