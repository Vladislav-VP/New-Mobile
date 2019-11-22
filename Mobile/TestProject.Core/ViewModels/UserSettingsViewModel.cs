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
        private TodoItem _user;

        private readonly IUserService _userService;

        public UserSettingsViewModel(IMvxNavigationService navigationService, IUserRepository userRepository,
            ICancelDialogService cancelDialogService, IUserService userService, IUserStorageHelper storage, IDialogsHelper dialogsHelper)
            : base(navigationService, storage, cancelDialogService, userRepository, dialogsHelper)
        {
            _userService = userService;

            UpdateUserCommand = new MvxAsyncCommand(HandleEntity);
            DeleteUserCommand = new MvxAsyncCommand(DeleteUser);
            EditPasswordCommand = new MvxAsyncCommand(EditPassword);
        }

        protected override bool IsStateChanged
        {
            get
            {
                return _user.Name != UserName;
            }
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

        public IMvxAsyncCommand UpdateUserCommand { get; private set; }

        public IMvxAsyncCommand DeleteUserCommand { get; private set; }

        public IMvxAsyncCommand EditPasswordCommand { get; private set; }        

        public async override Task Initialize()
        {
            await base.Initialize();

            //int userId = await _storage.Get();
            //_user = await _userService.Get(userId);
            //UserName = _user.Name;
        }

        protected override async Task HandleEntity()
        {

            //DataHandleResult<TodoItem> result = await _userService.EditUsername(_user, UserName);
            
            //if (result.IsSucceded)
            //{
            //    _dialogsHelper.DisplayToastMessage(Strings.UserNameChangedMessage);
            //    await _navigationService.Close(this);
            //}            
        }

        private async Task DeleteUser()
        {
            bool isConfirmedToDelete = await _dialogsHelper.IsConfirmed(Strings.DeleteMessageDialog);

            if (!isConfirmedToDelete)
            {
                return;
            }

            //await _userService.Delete(_user.Id);

            await _navigationService.Navigate<LoginViewModel>();
        }

        private async Task EditPassword()
        {
            await _navigationService.Navigate<EditPasswordViewModel>();
        }
    }
}
