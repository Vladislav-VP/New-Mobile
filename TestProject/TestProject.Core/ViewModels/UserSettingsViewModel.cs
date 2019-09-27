using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class UserSettingsViewModel : UserViewModel, IMvxViewModel<User, UpdateResult<User>>
    {
        public UserSettingsViewModel(IMvxNavigationService navigationService, IUserRepository userRepository, 
            IUserStorageHelper userStorage, IValidationHelper validationHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, userStorage, userRepository, validationHelper, dialogsHelper)
        {
            UpdateUserCommand = new MvxAsyncCommand(HandleEntity);
            DeleteUserCommand = new MvxAsyncCommand(DeleteUser);
            EditPasswordCommand = new MvxAsyncCommand(EditPassword);
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

        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged(() => User);
            }
        }

        public IMvxAsyncCommand UpdateUserCommand { get; private set; }

        public IMvxAsyncCommand DeleteUserCommand { get; private set; }

        public IMvxAsyncCommand EditPasswordCommand { get; private set; }

        protected override bool IsStateChanged
        {
            get => _user.Name != UserName;
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            UserName = _user.Name;
        }

        public void Prepare(User parameter)
        {
            User = parameter;
        }

        protected override async Task<bool> IsDataValid()
        {
            UserName = UserName.Trim();

            var userForChecking = new User
            {
                Name = UserName,
                Password = _user.Password
            };

            bool isUserNameValid = _validationHelper.IsObjectValid(userForChecking);
            if (!isUserNameValid)
            {
                return false;
            }

            User retrievedUser = await _userRepository.GetUser(UserName);
            if (retrievedUser != null && retrievedUser?.Id != _user.Id)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.UserAlreadyExistsMessage);
                return false;
            }
            
            return true;
        }
        protected override async Task HandleEntity()
        {
            bool isUserValid = await IsDataValid();
            if (!isUserValid)
            {
                return;
            }

            if (IsStateChanged)
            {
                _user.Name = UserName;
                await _userRepository.Update(_user);
                _dialogsHelper.DisplayToastMessage(Strings.UserNameChangedMessage);
            }

            UpdateResult<User> updateResult = GetUpdateResult(User);
            await _navigationService.Close(this, updateResult);
        }
        private async Task DeleteUser()
        {
            bool isToDelete = await _dialogsHelper.IsConfirmed(Strings.DeleteMessageDialog);

            if (!isToDelete)
            {
                return;
            }

            await _userRepository.Delete(_user);
            _storage.Clear();

            User = null;
            await _navigationService.Navigate<LoginViewModel>();
        }

        private async Task EditPassword()
        {
            await _navigationService.Navigate<EditPasswordViewModel>();
        }


    }
}
