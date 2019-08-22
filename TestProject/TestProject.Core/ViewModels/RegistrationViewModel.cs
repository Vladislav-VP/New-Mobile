using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class RegistrationViewModel : UserViewModel
    {
        public RegistrationViewModel(IMvxNavigationService navigationService, IUserRepository userRepository,
            IUserStorageHelper userStorage, IValidationHelper validationHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, userStorage, userRepository, validationHelper, dialogsHelper)
        {
            RegisterUserCommand = new MvxAsyncCommand(RegisterUser);
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

        public IMvxAsyncCommand RegisterUserCommand { get; private set; }

        private async Task RegisterUser()
        {
            User enteredUser = new User { Name = UserName, Password = Password };

            bool userIsValid = _validationHelper.ObjectIsValid<User>(enteredUser, nameof(enteredUser.Name)) 
                && _validationHelper.ObjectIsValid<User>(enteredUser, nameof(enteredUser.Password));
            bool validationErrorsEmpty = _validationHelper.ValidationErrors.Count == 0;
            if (!userIsValid && !validationErrorsEmpty)
            {
                _dialogsHelper.DisplayToastMessage(_validationHelper.ValidationErrors[0].ErrorMessage);
                return;
            }

            enteredUser.Name = enteredUser.Name.Trim();
            string query = _userRepository.GetUserQuery(enteredUser.Name);
            User userFromDataBase = await _userRepository.FindWithQuery(query);
            if (userFromDataBase != null)
            {
                _dialogsHelper.DisplayToastMessage(Strings.UserAlreadyExistsMessage);
                return;
            }

            await AddUser();
            await _navigationService.Navigate<TodoListItemViewModel>();
        }

        private async Task AddUser()
        {
            User user = new User { Name = UserName, Password = Password };
            await _userRepository.Insert(user);
            await _storage.Save(user.Id);
        }
    }
}
