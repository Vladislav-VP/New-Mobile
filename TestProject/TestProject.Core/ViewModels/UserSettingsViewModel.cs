using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class UserSettingsViewModel : UserViewModel
    {
        private User _user;

        private readonly IEditUsernameService _editUsernameService;

        public UserSettingsViewModel(IMvxNavigationService navigationService, IUserRepository userRepository,
            IEditUsernameService editUsernameService, IUserStorageHelper userStorage, IDialogsHelper dialogsHelper)
            : base(navigationService, userStorage, userRepository, dialogsHelper)
        {
            _editUsernameService = editUsernameService;

            UpdateUserCommand = new MvxAsyncCommand(HandleEntity);
            DeleteUserCommand = new MvxAsyncCommand(DeleteUser);
            EditPasswordCommand = new MvxAsyncCommand(EditPassword);
        }

        public string UserName
        {
            get => _user.Name;
            set
            {
                _user.Name = value;
                RaisePropertyChanged(() => UserName);
                IsStateChanged = true;
            }
        }

        public IMvxAsyncCommand UpdateUserCommand { get; private set; }

        public IMvxAsyncCommand DeleteUserCommand { get; private set; }

        public IMvxAsyncCommand EditPasswordCommand { get; private set; }        

        public async override Task Initialize()
        {
            await base.Initialize();

            _user = await _storage.Get();
            UserName = _user.Name;
            IsStateChanged = false;
        }

        protected override async Task HandleEntity()
        {
            var result = new EditUsernameResult { Data = _user };
            await _editUsernameService.EditUsername(result);

            if (result.IsSucceded)
            {
                _dialogsHelper.DisplayToastMessage(Strings.UserNameChangedMessage);
                await _navigationService.Close(this);
            }            
        }

        private async Task DeleteUser()
        {
            bool isToDelete = await _dialogsHelper.IsConfirmed(Strings.DeleteMessageDialog);

            if (!isToDelete)
            {
                return;
            }

            await _userRepository.Delete<User>(_user.Id);
            _storage.Clear();

            await _navigationService.Navigate<LoginViewModel>();
        }

        private async Task EditPassword()
        {
            await _navigationService.Navigate<EditPasswordViewModel>();
        }
    }
}
