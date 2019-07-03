using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.ViewModels;
using TestProject.Core.ViewModels;

namespace TestProject.Core
{
    
public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

            RegisterCustomAppStart<AppStart>();
        }
    }
}
