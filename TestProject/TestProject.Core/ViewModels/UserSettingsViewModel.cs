using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class UserSettingsViewModel : UserViewModel
    {
        private User _currentUser;

        protected string _userName;

        public UserSettingsViewModel(IMvxNavigationService navigationService, IUserRepository userRepository, 
            IStorageHelper<User> userStorage, IValidationHelper validationHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, userStorage, userRepository, validationHelper, dialogsHelper)
        {
            UserUpdatedCommand = new MvxAsyncCommand(UserUpdated);
            UserDeletedCommand = new MvxAsyncCommand(UserDeleted);
            ShowEditPasswordViewModelCommand = 
                new MvxAsyncCommand(async () => await _navigationService.Navigate<EditPasswordViewModel>());
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

        public IMvxAsyncCommand UserUpdatedCommand { get; private set; }

        public IMvxAsyncCommand UserDeletedCommand { get; private set; }

        public IMvxAsyncCommand ShowEditPasswordViewModelCommand { get; private set; }

        public async override Task Initialize()
        {
            await base.Initialize();

            _currentUser = await _storage.Retrieve();

            _userName = _currentUser.Name;
        }

        protected override async Task GoBack()
        {
            var result = await _navigationService.Navigate<CancelDialogViewModel, DialogResult>();

            if (result == DialogResult.Cancel)
            {
                return;
            }
            if (result == DialogResult.No)
            {
                await _navigationService.Navigate<TodoListItemViewModel>();
                return;
            }
            if (result == DialogResult.Yes)
            {
                await UserUpdated();
                return;
            }
        }

        private async Task<bool> TryUpdateUserName()
        {
            string currentUserName = _currentUser.Name;
            UserName = UserName.Trim();

            _currentUser.Name = UserName;
            bool userIsValid = _validationHelper.ObjectIsValid<User>(_currentUser, nameof(_currentUser.Name));
            bool validationErrorsEmpty = _validationHelper.ValidationErrors.Count == 0;
            if (!userIsValid && !validationErrorsEmpty)
            {
                _currentUser.Name = currentUserName;
                _dialogsHelper.ToastMessage(_validationHelper.ValidationErrors[0].ErrorMessage);
                return false;
            }

            bool userExists = await _userRepository.UserExists(UserName) && UserName != currentUserName;
            if (userExists)
            {
                _dialogsHelper.ToastMessage(Strings.UserAlreadyExistsMessage);
                return false;
            }

            await _userRepository.Update(_currentUser);
            return true;
        }

        private async Task UserUpdated()
        {
            if(!await TryUpdateUserName())
            {
                return;
            }

            _dialogsHelper.ToastMessage(Strings.UserNameChangedMessage);
            await _navigationService.Navigate<TodoListItemViewModel>();
            await _navigationService.Navigate<MenuViewModel>();
        }

        private async Task UserDeleted()
        {
            var delete = await _dialogsHelper.Confirm(Strings.DeleteMessageDialog);

            if (!delete)
            {
                return;
            }

            await _userRepository.Delete(_currentUser);
            _storage.Clear();

            var result = await _navigationService.Navigate<LoginViewModel>();
        }
    }
}
