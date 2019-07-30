using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.ViewModels;
using TestProject.Core.ViewModels;
using MvvmCross.IoC;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Services.Repositories;

namespace TestProject.Core
{
    
public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Repository")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.RegisterSingleton(typeof(IUserRepository), new UserRepository());
            Mvx.IoCProvider.RegisterSingleton(typeof(ITodoItemRepository), new TodoItemRepository());

            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

            RegisterCustomAppStart<AppStart>();
        }
    }
}
