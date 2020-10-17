using Microsoft.Extensions.DependencyInjection;
using aspnet_identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using aspnet_identity.Areas.Identity.Data;
using Microsoft.Extensions.Configuration;

namespace aspnet_identity.Configuration
{
  public static class IdentityConfig
  {
    public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
    {
      services.AddAuthorization(options =>
      {
        options.AddPolicy("PodeExcluir", policy => policy.RequireClaim("PodeExcluir"));

        options.AddPolicy("PodeLer", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeLer")));
        options.AddPolicy("PodeEscrever", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeEscrever")));
      });

      return services;
    }

    public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<AspNetCoreIdentityContext>(options =>
                  options.UseSqlite(
                      configuration.GetConnectionString("AspNetCoreIdentityContextConnection")));

      services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
          .AddRoles<IdentityRole>()
          .AddDefaultUI()
          .AddEntityFrameworkStores<AspNetCoreIdentityContext>();

      return services;
    }
  }
}