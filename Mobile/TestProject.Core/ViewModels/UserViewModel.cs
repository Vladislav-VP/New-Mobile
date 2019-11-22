using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class UserViewModel : BaseEntityViewModel
    {
        protected readonly IUserRepository _userRepository;

        protected readonly IStorageHelper<TodoItem> _userStorage;        

        public UserViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage,
            ICancelDialogService cancelDialogService, IUserRepository userRepository, IDialogsHelper dialogsHelper)
            : base(navigationService, storage, dialogsHelper, cancelDialogService)
        {
            _userRepository = userRepository;
        }
    }
}