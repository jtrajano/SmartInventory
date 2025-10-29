
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Application.Features.Auth.Login;
using SmartInventory.Domain.Entities;
using SmartInventory.Infrastructure.Data;
using SmartInventory.Infrastructure.Identity;
using SmartInventory.Infrastructure.Identity.Services;


namespace SmartInventory.Web.Endpoints;

public class Identity : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var grp = app.MapGroup(this);

        grp.MapPost("/register", Register);
        grp.MapPost("/login", Login);
        grp.MapGet("/auth-status", IsAuthenticated);
        
    }

    public IResult IsAuthenticated()
    {
        
        return Results.Ok(false);
    }

    public async Task<IResult> Register(RegisterRequest req, UserManager<ApplicationUser> userManager, ApplicationDbContext context )
    {

        var user = new ApplicationUser         {
            UserName = req.Email,
            Email = req.Email
        };
        await context.Database.BeginTransactionAsync();
            var result = await userManager.CreateAsync(user, req.Password);
            await userManager.AddToRoleAsync(user,"Member");
        await context.Database.CommitTransactionAsync();
        if (!result.Succeeded)
            return Results.BadRequest(result.Errors);

        return Results.Ok("User registered successfully");
    }

    public async Task<IResult> Login(LoginRequest req,
        SignInManager<ApplicationUser> signInManager,
        JwtTokenService jwtService,
        UserManager<ApplicationUser> userManager
        )
    {
        var user = await userManager.FindByEmailAsync(req.Email);

        if (user == null)
            return Results.Unauthorized();

        // Implement login logic here
        var result = await signInManager.CheckPasswordSignInAsync(user, req.Password, false);
        if(!result.Succeeded)
            return Results.Unauthorized();

        var roles = await userManager.GetRolesAsync(user);
        var token = jwtService.GenerateToken(user, roles);

        return Results.Ok(new LoginResponse(token, DateTime.Now.AddHours(2))); ;
    }
}
