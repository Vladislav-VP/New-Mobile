using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Services;

namespace TestProject.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            DependencyManager.ConfigureServices(services, Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            // TODO : Implement rewriting
            var options = new RewriteOptions().AddRewrite(
               @"/User/HomeInfo\?Id=((\w|\W)+)",  // RegEx to match Route
               "User/HomeInfo",                     // URL to rewrite route
               skipRemainingRules: false         // Should skip other rules
           ).AddRedirect(@"/User/HomeInfo\?Id=((\w|\W)+)",  // RegEx to match Route
               "User/HomeInfo", 301);
            app.UseRewriter(options);


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            // TODO : Rewrite routing logic.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=index}");
            });


        }
    }
}
