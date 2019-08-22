using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class LoginViewModel : UserViewModel
    {
        public LoginViewModel(IMvxNavigationService navigationService, IUserRepository userRepository,
            IUserStorageHelper userStorage, IValidationHelper validationHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, userStorage, userRepository, validationHelper, dialogsHelper)
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

        private async Task Login()
        {
            string query = _userRepository.GetUserQuery(UserName, Password);
            User userFromDatabase = await _userRepository.FindWithQuery(query);
            if (userFromDatabase == null)
            {
                _dialogsHelper.DisplayToastMessage(Strings.LoginErrorMessage);
                return;
            }

            await _storage.Save(userFromDatabase.Id);

            await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
