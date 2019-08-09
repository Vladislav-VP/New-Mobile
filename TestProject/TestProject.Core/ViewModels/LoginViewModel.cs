using TestProject.Entities;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Services.Helpers;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories;

namespace TestProject.Core.ViewModels
{
    public class LoginViewModel : UserViewModel
    {
        protected string _userName;
        protected string _password;

        public LoginViewModel(IMvxNavigationService navigationService, IUserRepository userRepository,
            IStorageHelper<User> userStorage, IValidationHelper validationHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, userStorage, userRepository, validationHelper, dialogsHelper)
        {
            LoginCommand = new MvxAsyncCommand(Login);
            ShowRegistrationScreenCommand = new MvxAsyncCommand(async ()
                => await _navigationService.Navigate<RegistrationViewModel>());
            ShowMenuCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

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
            var enteredUser = new User { Name = UserName, Password = Password };
            string query = Queries.GetUserQuery(enteredUser);
            var userFromDatabase = await _userRepository.FindWithQuery<User>(query);
            if (userFromDatabase == null)
            {
                _dialogsHelper.ToastMessage(Strings.LoginErrorMessage);
                return;
            }

            await _storage.Save(userFromDatabase.Id);

            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
