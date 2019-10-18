using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

using TestProject.Services;
using TestProject.Services.Helpers;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
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

            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx.IoCProvider.RegisterSingleton(typeof(IWebService), new WebService());
            Mvx.IoCProvider.RegisterSingleton(typeof(IUserRepository), new UserRepository());
            Mvx.IoCProvider.RegisterSingleton(typeof(ITodoItemRepository), new TodoItemRepository());
            Mvx.IoCProvider.RegisterSingleton(typeof(IDialogsHelper), new DialogsHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IPermissionsHelper), new PermissionsHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IEncryptionHelper), new EncryptionHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IPhotoCaptureHelper), new PhotoCaptureHelper());

            IDialogsHelper dialogsHelper = Mvx.IoCProvider.Resolve<IDialogsHelper>();
            Mvx.IoCProvider.RegisterSingleton(typeof(IValidationHelper), new ValidationHelper(dialogsHelper));

            IUserRepository userRepository = Mvx.IoCProvider.Resolve<IUserRepository>();
            Mvx.IoCProvider.RegisterSingleton(typeof(IUserStorageHelper), new UserStorageHelper(userRepository));

            IPermissionsHelper permissionsHelper = Mvx.IoCProvider.Resolve<IPermissionsHelper>();
            IPhotoCaptureHelper photoCaptureHelper = Mvx.IoCProvider.Resolve<IPhotoCaptureHelper>();
            IEncryptionHelper encryptionHelper = Mvx.IoCProvider.Resolve<IEncryptionHelper>();
            var photoEditHelper = new PhotoEditHelper(permissionsHelper, photoCaptureHelper, encryptionHelper);
            Mvx.IoCProvider.RegisterSingleton(typeof(IPhotoEditHelper), photoEditHelper);

            IValidationHelper validationHelper = Mvx.IoCProvider.Resolve<IValidationHelper>();
            IUserStorageHelper storage = Mvx.IoCProvider.Resolve<IUserStorageHelper>();

            var userService = new UserService(validationHelper, dialogsHelper, userRepository, storage);
            Mvx.IoCProvider.RegisterSingleton(typeof(IUserService), userService);

            RegisterCustomAppStart<AppStart>();
        }
    }
}
