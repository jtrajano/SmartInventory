using SmartInventory.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace SmartInventory.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly IUser<Guid?> _user;
    private readonly IIdentityService<Guid> _identityService;

    public LoggingBehaviour(ILogger<TRequest> logger, IUser<Guid?> user, IIdentityService<Guid> identityService)
    {
        _logger = logger;
        _user = user;
        _identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _user.Id ?? Guid.Empty;
        string? userName = string.Empty;

        if (Guid.Empty != userId)
        {
            userName = await _identityService.GetUserNameAsync(userId);
        }

        _logger.LogInformation("SmartInventory Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
    }
}
