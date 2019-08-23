using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class UserViewModel : BaseEntityViewModel
    {
        protected readonly IUserRepository _userRepository;

        protected readonly IStorageHelper<User> _userStorage;        

        public UserViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage, IUserRepository userRepository,
             IValidationHelper validationHelper, IValidationResultHelper validationResultHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, storage, dialogsHelper, validationHelper, validationResultHelper)
        {
            _userRepository = userRepository;
        }
    }
}