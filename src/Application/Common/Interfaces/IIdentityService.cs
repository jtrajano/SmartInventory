using SmartInventory.Application.Common.Models;

namespace SmartInventory.Application.Common.Interfaces;

public interface IIdentityService<T>
{
    Task<string?> GetUserNameAsync(T userId);

    Task<bool> IsInRoleAsync(T userId, string role);

    Task<bool> AuthorizeAsync(T userId, string policyName);

    Task<(Result Result, T UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(T userId);
}
