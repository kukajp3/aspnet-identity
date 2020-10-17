using Microsoft.Extensions.DependencyInjection;
using aspnet_identity.Extensions;
using Microsoft.AspNetCore.Authorization;
using KissLog;
using Microsoft.AspNetCore.Http;

namespace aspnet_identity.Configuration
{
  public static class DependencyInjectionConfig
  {
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
      services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();

      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddScoped<ILogger>((context) => Logger.Factory.Get());

      services.AddScoped<AuditoriaFilter>();

      return services;
    }
  }
}