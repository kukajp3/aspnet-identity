using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using aspnet_identity.Configuration;
using aspnet_identity.Extensions;
using KissLog.AspNetCore;

namespace aspnet_identity
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IWebHostEnvironment hostingEnvironment)
    {
      var builder = new ConfigurationBuilder()
      .SetBasePath(hostingEnvironment.ContentRootPath)
      .AddJsonFile("appsettings.json", true, true)
      .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true, true)
      .AddEnvironmentVariables();

      Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews();

      services.AddIdentityConfig(Configuration);

      services.AddAuthorizationConfig();

      services.ResolveDependencies();

      services.AddMvc(options => options.Filters.Add(typeof(AuditoriaFilter)));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/erro/500");
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseKissLogMiddleware(options =>
      {
        new LogConfig().ConfigureKissLog(options, Configuration);
      });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapRazorPages();
      });
    }
  }
}
