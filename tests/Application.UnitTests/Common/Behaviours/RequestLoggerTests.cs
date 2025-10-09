using SmartInventory.Application.Common.Behaviours;
using SmartInventory.Application.Common.Interfaces;
using SmartInventory.Application.TodoItems.Commands.CreateTodoItem;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace SmartInventory.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateTodoItemCommand>> _logger = null!;
    private Mock<IUser<Guid?>> _user = null!;
    private Mock<IIdentityService<Guid>> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateTodoItemCommand>>();
        _user = new Mock<IUser<Guid?>>();
        _identityService = new Mock<IIdentityService<Guid>>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _user.Setup(x => x.Id).Returns(Guid.NewGuid());

        var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<Guid>()), Times.Never);
    }
}
