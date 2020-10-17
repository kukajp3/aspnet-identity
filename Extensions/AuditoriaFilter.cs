using Microsoft.AspNetCore.Mvc.Filters;
using KissLog;

namespace aspnet_identity.Extensions
{
  public class AuditoriaFilter : IActionFilter
  {
    private readonly ILogger _logger;

    public AuditoriaFilter(ILogger logger)
    {
      _logger = logger;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
      if (context.HttpContext.User.Identity.IsAuthenticated)
      {
        var message = context.HttpContext.User.Identity.Name + " acessou: ";

        _logger.Info(message);
      }
    }
  }
}