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
            IUserStorageHelper userStorage, IValidationHelper validationHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, userStorage, userRepository, validationHelper, dialogsHelper)
        {
            UpdateUserCommand = new MvxAsyncCommand(UpdateUser);
            DeleteUserCommand = new MvxAsyncCommand(DeleteUser);
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

        public IMvxAsyncCommand UpdateUserCommand { get; private set; }

        public IMvxAsyncCommand DeleteUserCommand { get; private set; }

        public IMvxAsyncCommand ShowEditPasswordViewModelCommand { get; private set; }

        public async override Task Initialize()
        {
            await base.Initialize();

            _currentUser = await _storage.Get();

            _userName = _currentUser.Name;
        }

        protected override async Task GoBack()
        {
            DialogResult result = await _navigationService.Navigate<CancelDialogViewModel, DialogResult>();

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
                await UpdateUser();
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
                _dialogsHelper.DisplayToastMessage(_validationHelper.ValidationErrors[0].ErrorMessage);
                return false;
            }

            string query = _userRepository.GetUserQuery(UserName);
            User userFromDataBase = await _userRepository.FindWithQuery(query);
            if (userFromDataBase != null && userFromDataBase.Id != _currentUser.Id)
            {
                _dialogsHelper.DisplayToastMessage(Strings.UserAlreadyExistsMessage);
                return false;
            }

            await _userRepository.Update(_currentUser);
            return true;
        }

        private async Task UpdateUser()
        {
            if(!await TryUpdateUserName())
            {
                return;
            }

            _dialogsHelper.DisplayToastMessage(Strings.UserNameChangedMessage);
            await _navigationService.Navigate<TodoListItemViewModel>();
            // TODO: Correcrt issue with navigation to menu (username is not updated without navigating).
            await _navigationService.Navigate<MenuViewModel>();
        }

        private async Task DeleteUser()
        {
            bool isToDelete = await _dialogsHelper.TryGetConfirmation(Strings.DeleteMessageDialog);

            if (!isToDelete)
            {
                return;
            }

            await _userRepository.Delete(_currentUser);
            _storage.Clear();

            await _navigationService.Navigate<LoginViewModel>();
        }
    }
}
