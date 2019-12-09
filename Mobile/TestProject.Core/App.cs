using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

using TestProject.Services;
using TestProject.Services.Helpers;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

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
            Mvx.IoCProvider.RegisterSingleton(typeof(IDialogsHelper), new DialogsHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IPermissionsHelper), new PermissionsHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IEncryptionHelper), new EncryptionHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IPhotoCaptureHelper), new PhotoCaptureHelper());

            IDialogsHelper dialogsHelper = Mvx.IoCProvider.Resolve<IDialogsHelper>();
            Mvx.IoCProvider.RegisterSingleton(typeof(IValidationHelper), new ValidationHelper(dialogsHelper));

            Mvx.IoCProvider.RegisterSingleton(typeof(IStorageHelper), new StorageHelper());

            IPermissionsHelper permissionsHelper = Mvx.IoCProvider.Resolve<IPermissionsHelper>();
            IPhotoCaptureHelper photoCaptureHelper = Mvx.IoCProvider.Resolve<IPhotoCaptureHelper>();
            IEncryptionHelper encryptionHelper = Mvx.IoCProvider.Resolve<IEncryptionHelper>();
            var photoEditHelper = new PhotoEditHelper(permissionsHelper, photoCaptureHelper, encryptionHelper);
            Mvx.IoCProvider.RegisterSingleton(typeof(IPhotoEditHelper), photoEditHelper);

            IValidationHelper validationHelper = Mvx.IoCProvider.Resolve<IValidationHelper>();
            IStorageHelper storage = Mvx.IoCProvider.Resolve<IStorageHelper>();

            var userService = new UserService(validationHelper, dialogsHelper, storage, photoEditHelper);
            Mvx.IoCProvider.RegisterSingleton(typeof(IUserService), userService);

            var todoItemService = new TodoItemService(validationHelper, dialogsHelper, storage);
            Mvx.IoCProvider.RegisterSingleton(typeof(ITodoItemService), todoItemService);

            RegisterCustomAppStart<AppStart>();
        }
    }
}
