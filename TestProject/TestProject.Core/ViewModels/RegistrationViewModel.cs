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
        public RegistrationViewModel(IMvxNavigationService navigationService, IUserRepository userRepository, IUserStorageHelper storage,
             IValidationHelper validationHelper, IValidationResultHelper validationResultHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, storage, userRepository, validationHelper, validationResultHelper, dialogsHelper)
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

        protected override async Task<bool> TryValidateData()
        {
            User enteredUser = new User { Name = UserName, Password = Password };

            bool isUserDataValid = _validationHelper.TryValidateObject(enteredUser, nameof(enteredUser.Name))
                && _validationHelper.TryValidateObject(enteredUser, nameof(enteredUser.Password));
            if (!isUserDataValid)
            {
                _validationResultHelper.HandleValidationResult(enteredUser, nameof(enteredUser.Name));
                _validationResultHelper.HandleValidationResult(enteredUser, nameof(enteredUser.Password));
                return false;
            }

            enteredUser.Name = enteredUser.Name.Trim();
            string query = _userRepository.GetUserQuery(enteredUser.Name);
            User userFromDataBase = await _userRepository.FindWithQuery(query);
            if (userFromDataBase != null)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.UserAlreadyExistsMessage);
                return false;
            }

            return true;
        }

        private async Task RegisterUser()
        {
            bool isUserValid = await TryValidateData();
            if (!isUserValid)
            {
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
