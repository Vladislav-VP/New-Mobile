using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class LoginViewModel : UserViewModel
    {
        private User _currentUser;

        public LoginViewModel(IMvxNavigationService navigationService, IUserRepository userRepository, IUserStorageHelper storage,
             IValidationHelper validationHelper, IValidationResultHelper validationResultHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, storage, userRepository, validationHelper, validationResultHelper, dialogsHelper)
        {
            LoginCommand = new MvxAsyncCommand(Login);
            ShowRegistrationScreenCommand = new MvxAsyncCommand(async ()
                => await _navigationService.Navigate<RegistrationViewModel>());
            ShowMenuCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public IMvxAsyncCommand LoginCommand { get; private set; }
        
        public IMvxAsyncCommand ShowRegistrationScreenCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuCommand { get; private set; }

        protected override async Task<bool> TryValidateData()
        {
            string query = _userRepository.GetUserQuery(UserName, Password);
            _currentUser = await _userRepository.FindWithQuery(query);
            return _currentUser != null;
        }

        private async Task Login()
        {
            bool isUserDataValid = await TryValidateData();
            if (isUserDataValid)
            {
                _dialogsHelper.DisplayToastMessage(Strings.LoginErrorMessage);
                return;
            }

            await _storage.Save(_currentUser.Id);
            await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
