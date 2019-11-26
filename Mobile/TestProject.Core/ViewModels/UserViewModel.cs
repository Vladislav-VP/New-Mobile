using MvvmCross.Navigation;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class UserViewModel : BaseEntityViewModel
    {

        protected readonly IStorageHelper _userStorage;        

        public UserViewModel(IMvxNavigationService navigationService, IStorageHelper storage,
            ICancelDialogService cancelDialogService,  IDialogsHelper dialogsHelper)
            : base(navigationService, storage, dialogsHelper, cancelDialogService)
        {
        }
    }
}