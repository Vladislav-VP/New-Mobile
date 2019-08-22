using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class UserViewModel : BaseViewModel
    {
        protected readonly IUserRepository _userRepository;

        protected readonly IStorageHelper<User> _userStorage;

        protected readonly IValidationHelper _validationHelper;

        protected readonly IDialogsHelper _dialogsHelper;

        public UserViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage,
            IUserRepository userRepository, IValidationHelper validationHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, storage)
        {
            _userRepository = userRepository;
            _validationHelper = validationHelper;
            _dialogsHelper = dialogsHelper;
        }
    }
}