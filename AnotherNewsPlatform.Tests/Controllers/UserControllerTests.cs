using System.Security.Claims;
using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.MVC.Controllers;
using AnotherNewsPlatform.MVC.Mappers;
using AnotherNewsPlatform.MVC.Models.User;
using AnotherNewsPlatform.Services.UserService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace AnotherNewsPlatform.Tests.Controllers;

public class UserControllerTests
{
    private readonly Mock<AnpDbContext> _dbContextMock;
    private readonly Mock<ILogger<UserController>> _loggerMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UserMapper _mapper;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AnpDbContext>();
        _dbContextMock = new Mock<AnpDbContext>(optionsBuilder.Options);
        _loggerMock = new Mock<ILogger<UserController>>();
        _userServiceMock = new Mock<IUserService>();
        _mapper = new UserMapper();

        _controller = new UserController(
            _dbContextMock.Object,
            _loggerMock.Object,
            _userServiceMock.Object,
            _mapper);

        // Set up a default HttpContext with mocked authentication service
        var httpContext = new DefaultHttpContext();
        var authServiceMock = new Mock<IAuthenticationService>();
        authServiceMock
            .Setup(x => x.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
            .Returns(Task.CompletedTask);
        authServiceMock
            .Setup(x => x.SignOutAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<AuthenticationProperties>()))
            .Returns(Task.CompletedTask);

        httpContext.RequestServices = new ServiceCollection()
            .AddSingleton(authServiceMock.Object)
            .AddMvcCore()
            .AddViews()
            .Services
            .BuildServiceProvider();

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }

    #region Login (GET)

    [Fact]
    public void Login_Get_ReturnsViewResult()
    {
        // Act
        var result = _controller.Login("/some-return-url");

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
    }

    #endregion

    #region LoginProcess (POST)

    [Fact]
    public async Task LoginProcess_WhenModelIsInvalid_ReturnsNotFound()
    {
        // Arrange
        _controller.ModelState.AddModelError("Email", "Required");
        var model = new LoginModel { Email = "", Password = "" };

        // Act
        var result = await _controller.LoginProcess(model, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _userServiceMock.Verify(x => x.VerifyUserAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task LoginProcess_WhenUserVerificationFails_ReturnsNotFound()
    {
        // Arrange
        var model = new LoginModel { Email = "test@example.com", Password = "wrong" };
        _userServiceMock
            .Setup(x => x.VerifyUserAsync(model.Email, model.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.LoginProcess(model, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _userServiceMock.Verify(x => x.GetLoginDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task LoginProcess_WhenGetLoginDataReturnsNull_ReturnsNotFound()
    {
        // Arrange
        var model = new LoginModel { Email = "test@example.com", Password = "password123" };
        _userServiceMock
            .Setup(x => x.VerifyUserAsync(model.Email, model.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _userServiceMock
            .Setup(x => x.GetLoginDataAsync(model.Email, model.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ClaimsIdentity?)null);

        // Act
        var result = await _controller.LoginProcess(model, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task LoginProcess_WhenValidCredentials_RedirectsToNewsIndex()
    {
        // Arrange
        var model = new LoginModel { Email = "test@example.com", Password = "password123" };
        var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "test") }, "cookie");

        _userServiceMock
            .Setup(x => x.VerifyUserAsync(model.Email, model.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _userServiceMock
            .Setup(x => x.GetLoginDataAsync(model.Email, model.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(claimsIdentity);

        // Act
        var result = await _controller.LoginProcess(model, CancellationToken.None);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("News", redirectResult.ControllerName);
    }

    [Fact]
    public async Task LoginProcess_WhenCancellationTokenIsCancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var model = new LoginModel { Email = "test@example.com", Password = "password123" };
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        _userServiceMock
            .Setup(x => x.VerifyUserAsync(model.Email, model.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _userServiceMock
            .Setup(x => x.GetLoginDataAsync(model.Email, model.Password, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            _controller.LoginProcess(model, cancellationTokenSource.Token));
    }

    #endregion

    #region Logout

    [Fact]
    public async Task Logout_SignsOutAndRedirectsToHomeIndex()
    {
        // Act
        var result = await _controller.Logout();

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("Home", redirectResult.ControllerName);
    }

    #endregion

    #region Register (GET)

    [Fact]
    public void Register_Get_ReturnsViewResult()
    {
        // Act
        var result = _controller.Register();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
    }

    #endregion

    #region RegisterProcessing (POST)

    [Fact]
    public async Task RegisterProcessing_WhenModelIsValid_CallsRegisterAsyncAndReturnsView()
    {
        // Arrange
        var model = new RegisterModel
        {
            Username = "newuser",
            Email = "newuser@example.com",
            Password = "Password123!",
            ConfirmPassword = "Password123!"
        };

        // Act
        var result = await _controller.RegisterProcessing(model, CancellationToken.None);

        // Assert
        _userServiceMock.Verify(
            x => x.RegisterAsync(model.Username, model.Email, model.Password, It.IsAny<CancellationToken>()),
            Times.Once);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Register", viewResult.ViewName);
        Assert.Same(model, viewResult.Model);
    }

    [Fact]
    public async Task RegisterProcessing_WhenModelIsInvalid_DoesNotCallRegisterAndReturnsView()
    {
        // Arrange
        _controller.ModelState.AddModelError("Email", "Required");
        var model = new RegisterModel
        {
            Username = "newuser",
            Email = "",
            Password = "Password123!",
            ConfirmPassword = "Password123!"
        };

        // Act
        var result = await _controller.RegisterProcessing(model, CancellationToken.None);

        // Assert
        _userServiceMock.Verify(
            x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Register", viewResult.ViewName);
        Assert.Same(model, viewResult.Model);
    }

    [Fact]
    public async Task RegisterProcessing_WhenCancellationTokenIsCancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var model = new RegisterModel
        {
            Username = "newuser",
            Email = "newuser@example.com",
            Password = "Password123!",
            ConfirmPassword = "Password123!"
        };
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        _userServiceMock
            .Setup(x => x.RegisterAsync(model.Username, model.Email, model.Password, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            _controller.RegisterProcessing(model, cancellationTokenSource.Token));
    }

    #endregion

    #region VerifyEmail

    [Fact]
    public async Task VerifyEmail_WhenEmailExists_ReturnsTrue()
    {
        // Arrange
        var email = "existing@example.com";
        _userServiceMock
            .Setup(x => x.VerifyEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.VerifyEmail(email, CancellationToken.None);

        // Assert
        var jsonResult = Assert.IsType<JsonResult>(result);
        Assert.True((bool)jsonResult.Value!);
    }

    [Fact]
    public async Task VerifyEmail_WhenEmailDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var email = "nonexistent@example.com";
        _userServiceMock
            .Setup(x => x.VerifyEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.VerifyEmail(email, CancellationToken.None);

        // Assert
        var jsonResult = Assert.IsType<JsonResult>(result);
        Assert.False((bool)jsonResult.Value!);
    }

    [Fact]
    public async Task VerifyEmail_WhenCancellationTokenIsCancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var email = "test@example.com";
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        _userServiceMock
            .Setup(x => x.VerifyEmailAsync(email, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            _controller.VerifyEmail(email, cancellationTokenSource.Token));
    }

    #endregion

    #region Profile

    [Fact]
    public async Task Profile_WhenUserExists_ReturnsViewWithMappedModel()
    {
        // Arrange
        var userId = 42L;
        var userDto = new UserDto
        {
            Id = userId,
            Username = "testuser",
            Email = "test@example.com",
            PasswordHash = "hash",
            RoleId = 1
        };

        _userServiceMock
            .Setup(x => x.GetUserDtoAsync(userId))
            .ReturnsAsync(userDto);

        // Act
        var result = await _controller.Profile(userId.ToString());

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ChangeUserModel>(viewResult.Model);

        Assert.Equal(userDto.Username, model.Username);
        Assert.Equal(userDto.Email, model.Email);
    }

    [Fact]
    public async Task Profile_WhenIdIsInvalid_ThrowsFormatException()
    {
        // Arrange
        var invalidId = "not-a-number";

        // Act & Assert
        await Assert.ThrowsAsync<FormatException>(() =>
            _controller.Profile(invalidId));
    }

    [Fact]
    public async Task Profile_WhenUserDoesNotExist_ThrowsNullReferenceException()
    {
        // Arrange
        var userId = 999L;
        _userServiceMock
            .Setup(x => x.GetUserDtoAsync(userId))
            .ReturnsAsync((UserDto?)null!);

        // Act & Assert
        // The controller passes null to the Mapperly-generated mapper, which throws NRE
        await Assert.ThrowsAsync<NullReferenceException>(() =>
            _controller.Profile(userId.ToString()));
    }

    #endregion

    #region ChangeUserProcessing (PATCH)

    [Fact]
    public async Task ChangeUserProcessing_CallsUpdateUserAndRedirectsToHomeIndex()
    {
        // Arrange
        var model = new ChangeUserModel(1)
        {
            Username = "updateduser",
            Email = "updated@example.com",
            Password = "NewPassword123!",
            ConfirmPassword = "NewPassword123!"
        };

        // Act
        var result = await _controller.ChangeUserProcessing(model);

        // Assert
        _userServiceMock.Verify(
            x => x.UpdateUserAsync(It.Is<UserDto>(dto =>
                dto.Username == model.Username &&
                dto.Email == model.Email)),
            Times.Once);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("Home", redirectResult.ControllerName);
    }

    #endregion

    #region DeleteUser (DELETE)

    [Fact]
    public async Task DeleteUser_CallsDeleteUserAndRedirectsToLogout()
    {
        // Arrange
        var userId = 42L;

        // Act
        var result = await _controller.DeleteUser(userId);

        // Assert
        _userServiceMock.Verify(
            x => x.DeleteUserAsync(userId),
            Times.Once);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Logout", redirectResult.ActionName);
        Assert.Equal("Home", redirectResult.ControllerName);
    }

    #endregion
}