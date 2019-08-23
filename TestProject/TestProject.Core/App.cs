using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

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
            Mvx.IoCProvider.RegisterSingleton(typeof(IDialogsHelper), new DialogsHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IUserDialogsHelper), new UserDialogsHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IUserStorageHelper), new UserStorageHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IPermissionsHelper), new PermissionsHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IEncryptionHelper), new EncryptionHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IPhotoCaptureHelper), new PhotoCaptureHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IPhotoEditHelper), new PhotoEditHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IValidationResultHelper), new ValidationResultHelper());

            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

            RegisterCustomAppStart<AppStart>();
        }
    }
}
