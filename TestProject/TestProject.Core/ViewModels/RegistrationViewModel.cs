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
        protected string _userName;
        protected string _password;

        public RegistrationViewModel(IMvxNavigationService navigationService, IUserRepository userRepository,
            IStorageHelper<User> userStorage, IValidationHelper validationHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, userStorage, userRepository, validationHelper, dialogsHelper)
        {

            RegistrateUserCommand = new MvxAsyncCommand(RegistrateUser);
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

        public IMvxAsyncCommand RegistrateUserCommand { get; private set; }

        private async Task RegistrateUser()
        {
            User user = new User { Name = UserName, Password = Password };

            bool userIsValid = _validationHelper.ObjectIsValid<User>(user, nameof(user.Name)) 
                && _validationHelper.ObjectIsValid<User>(user, nameof(user.Password));
            bool validationErrorsEmpty = _validationHelper.ValidationErrors.Count == 0;
            if (!userIsValid && !validationErrorsEmpty)
            {
                _dialogsHelper.ToastMessage(_validationHelper.ValidationErrors[0].ErrorMessage);
                return;
            }

            user.Name = user.Name.Trim();
            bool userExists = await _userRepository.UserExists(user.Name);
            if (userExists)
            {
                _dialogsHelper.ToastMessage(Strings.UserAlreadyExistsMessage);
                return;
            }

            await AddUser();
            var users = await _userRepository.GetAllObjects<User>();
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
