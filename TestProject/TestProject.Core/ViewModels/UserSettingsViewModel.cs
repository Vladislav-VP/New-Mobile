using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.Enums;
using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class UserSettingsViewModel : UserViewModel, IMvxViewModel<User, UpdateResult<User>>
    {
        protected string _oldUserName;
        protected string _newUserName;

        public UserSettingsViewModel(IMvxNavigationService navigationService, IUserRepository userRepository, 
            IUserStorageHelper userStorage, IValidationHelper validationHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, userStorage, userRepository, validationHelper, dialogsHelper)
        {
            UpdateUserCommand = new MvxAsyncCommand(UpdateUser);
            DeleteUserCommand = new MvxAsyncCommand(DeleteUser);
            EditPasswordCommand = new MvxAsyncCommand(EditPassword);
        }

        public string NewUserName
        {
            get => _newUserName;
            set
            {
                _newUserName = value;
                RaisePropertyChanged(() => NewUserName);
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
            get => _oldUserName != _newUserName;
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            _oldUserName = _user.Name;
            _newUserName = _user.Name;
        }

        public void Prepare(User parameter)
        {
            User = parameter;
        }

        protected override async Task GoBack()
        {
            if (!IsStateChanged)
            {
                await _navigationService.Navigate<TodoItemListViewModel>();
                return;
            }

            YesNoCancelDialogResult result = await _navigationService
                .Navigate<CancelDialogViewModel, YesNoCancelDialogResult>();

            if (result == YesNoCancelDialogResult.Yes)
            {
                await UpdateUser();
                return;
            }

            await HandleDialogResult(result);
        }

        protected override async Task<bool> IsDataValid()
        {
            string oldUserName = _user.Name;
            NewUserName = NewUserName.Trim();

            _user.Name = NewUserName;
            bool isUserNameValid = _validationHelper.IsObjectValid(_user);
            if (!isUserNameValid)
            {                
                _user.Name = oldUserName;
                NewUserName = oldUserName;
                return false;
            }

            User retrievedUser = await _userRepository.GetUser(NewUserName);
            if (retrievedUser != null && retrievedUser.Id != _user.Id)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.UserAlreadyExistsMessage);
                _user.Name = oldUserName;
                NewUserName = oldUserName;
                return false;
            }

            return true;
        }

        private async Task UpdateUser()
        {
            bool isUserValid = await IsDataValid();
            if (!isUserValid)
            {
                return;
            }

            if (IsStateChanged)
            {
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

            await _navigationService.Navigate<LoginViewModel>();
        }

        private async Task EditPassword()
        {
            await _navigationService.Navigate<EditPasswordViewModel>();
        }
    }
}
