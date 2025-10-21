
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using SmartInventory.Infrastructure.Identity;


namespace SmartInventory.Web.Endpoints;

public class Identity : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost("/register", Register);
        app.MapGroup(this)
            .MapPost("/login", Login);
    }

    public async Task<IResult> Register(RegisterRequest req, UserManager<ApplicationUser> userManager )
    {
        var user = new ApplicationUser         {
            UserName = req.Email,
            Email = req.Email
        };
        var result = await userManager.CreateAsync(user, req.Password);

        if(!result.Succeeded)
            return Results.BadRequest(result.Errors);

        return Results.Ok("User registered successfully");
    }

    public async Task<IResult> Login(LoginRequest req, SignInManager<ApplicationUser> signInManager)
    {
        // Implement login logic here
        var result = await signInManager.PasswordSignInAsync(req.Email, req.Password, isPersistent: false, lockoutOnFailure: false);
        if(!result.Succeeded)
            return Results.Unauthorized();



        return Results.Ok("Login successful");
    }
}
