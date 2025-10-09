using Microsoft.AspNetCore.Identity;

namespace SmartInventory.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string Role { get; set; } = "Employee";
}
