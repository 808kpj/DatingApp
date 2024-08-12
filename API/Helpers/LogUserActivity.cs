using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API;

//ActionFilter happens with actions and Actions are scoped to httpRequests
public class LogUserActivity : IAsyncActionFilter
{
    //any code before ActionExecutionDelegate next will execute before the api controller class is called after next it will execute after the api controller class
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (context.HttpContext.User.Identity?.IsAuthenticated != true) return;

        var userId = resultContext.HttpContext.User.GetUserId();

        var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
        var user = await repo.GetByIdAsync(userId);
        if (user == null) return;

        //updates when the user was last active
        user.LastActive = DateTime.UtcNow;
        await repo.SaveAllAsync();
    }
}
