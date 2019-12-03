using Microsoft.Extensions.DependencyInjection;

using Services.Interfaces;
using Services.UI;

namespace Services
{
    public static class DependencyManager
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            DataAccess.DependencyManager.ConfigureServices(services);
            services.AddTransient<IUsersService, UsersService>();  
            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<IImageService, ImageService>();
        }
    }
}
