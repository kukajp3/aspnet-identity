using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aspnet_identity.Controllers
{
  public class TesteController : Controller
  {
    private readonly ILogger<TesteController> _logger;

    public TesteController(ILogger<TesteController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      _logger.LogDebug("Erro");
      _logger.LogTrace("Acesso");

      return View();
    }
  }
}