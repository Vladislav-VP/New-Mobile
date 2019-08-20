using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

using TestProject.Entities;
using TestProject.Services.Helpers;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories;
using TestProject.Services.Repositories.Interfaces;

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
            Mvx.IoCProvider.RegisterSingleton(typeof(IValidationHelper), new ValidationHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IDialogsHelper), new UserDialogsHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IStorageHelper<User>), new StorageHelper());
            
            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

            RegisterCustomAppStart<AppStart>();
        }
    }
}
