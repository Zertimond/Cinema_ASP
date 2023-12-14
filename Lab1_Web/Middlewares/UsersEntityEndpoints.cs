using Common.Models;
using Lab1_Web.Services;

namespace Lab1_Web.Middlewares;

public class UserEntityEndpoints
{
    private readonly RequestDelegate _next;

    public UserEntityEndpoints(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userService = context.RequestServices.GetService<IUserService>()
        ?? throw new Exception("Cannot resolve IUserService");

        var path = context.Request.Path;
        switch (path)
        {
            case "/users":
            {
                // var users = await userService.GetAllUsers();
                // await context.Response.WriteAsJsonAsync(users);
                break;
            }
            case "/users/create":
            {
                var model = await context.Request.ReadFromJsonAsync<UserCreateModel>()
                            ?? throw new Exception("Invalid user model");
                var userId = await userService.CreateUser(model);
                await context.Response.WriteAsJsonAsync(userId);
                break;
            }
            case "/users/update":
            {
                var model = await context.Request.ReadFromJsonAsync<UserUpdateModel>()
                            ?? throw new Exception("Invalid user model");
                await userService.UpdateUser(model);
                await context.Response.WriteAsync("User updated");
                break;
            }
            case "/users/delete":
            {
                var model = await context.Request.ReadFromJsonAsync<UserDeleteModel>()
                            ?? throw new Exception("Invalid user model");
                await userService.DeleteUser(model);
                await context.Response.WriteAsync("User deleted");
                break;
            }
        }
    }
}

public static class UserEntityEndpointsExtensions
{
    public static IApplicationBuilder UseUserEntityEndpoints(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserEntityEndpoints>();
    }
}