using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using aspnet_identity.Models;
using aspnet_identity.Extensions;

namespace aspnet_identity.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      return View();
    }

    [Authorize]
    public IActionResult Privacy()
    {
      return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Secret()
    {
      return View();
    }

    [Authorize(Policy = "PodeExcluir")]
    public IActionResult SecretClaim()
    {
      return View();
    }

    [Authorize(Policy = "PodeEscrever")]
    public IActionResult SecretClaimGravar()
    {
      return View();
    }

    [ClaimsAuthorize("Produtos", "Ler")]
    public IActionResult ClaimCustom()
    {
      return View();
    }

    [Route("erro/{id:length(3,3)}")]
    public IActionResult Error(int id)
    {
      var modelErro = new ErrorViewModel();

      if (id == 500)
      {
        modelErro.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
        modelErro.Title = "Ocorreu um erro!";
        modelErro.Error = id;
      }
      else if (id == 404)
      {
        modelErro.Message = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
        modelErro.Title = "Ops! Página não encontrada.";
        modelErro.Error = id;
      }
      else if (id == 403)
      {
        modelErro.Message = "Você não tem permissão para fazer isto.";
        modelErro.Title = "Acesso Negado";
        modelErro.Error = id;
      }
      else
      {
        return StatusCode(404);
      }

      return View("Error", modelErro);
    }
  }
}
