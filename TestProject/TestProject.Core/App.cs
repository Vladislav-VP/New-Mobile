using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.ViewModels;
using TestProject.Core.ViewModels;
using MvvmCross.IoC;

namespace TestProject.Core
{
    
public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

            RegisterCustomAppStart<AppStart>();
        }
    }
}
