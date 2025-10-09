using System.Security.Claims;

using SmartInventory.Application.Common.Interfaces;

namespace SmartInventory.Web.Services;

public class CurrentUser : IUser<Guid?>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Guid? Id { get {
            var id = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null) return null;
            return new Guid(id);
        } 
    
    }
   // public Guid? Id { get { return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)} }
}
