using Microsoft.AspNetCore.Mvc.Filters;
using Service.Abstraction.Services;
using TestWebApi.Extensions;

namespace TestWebApi.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity!.IsAuthenticated) return;

            var userId = int.Parse(resultContext.HttpContext.User.GetUserId());

            var userService = resultContext.HttpContext.RequestServices.GetRequiredService<IUserService>();
            var user = userService.GetUserById(userId);
            user.LastActive = DateTime.UtcNow;
            userService.UpdateUser(user.UserName, user);
        }
    }
}
