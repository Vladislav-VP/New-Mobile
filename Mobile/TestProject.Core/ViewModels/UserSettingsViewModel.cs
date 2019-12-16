using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.ApiModels.User;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class UserSettingsViewModel : UserViewModel
    {
        private string _oldUserName;
        private string _oldEmail;

        private readonly IUserService _userService;

        public UserSettingsViewModel(IMvxNavigationService navigationService, ICancelDialogService cancelDialogService,
            IUserService userService, IStorageHelper storage, IDialogsHelper dialogsHelper)
            : base(navigationService, storage, cancelDialogService, dialogsHelper)
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
                return _oldUserName != UserName
                    || _oldEmail != Email;
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

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        } 

        public IMvxAsyncCommand UpdateUserCommand { get; private set; }

        public IMvxAsyncCommand DeleteUserCommand { get; private set; }

        public IMvxAsyncCommand EditPasswordCommand { get; private set; }        

        public async override Task Initialize()
        {
            await base.Initialize();

            GetUserInfoUserApiModel userInfo = await _userService.GetUserInfo();
            UserName = userInfo.UserName;
            Email = userInfo.Email;
            _oldUserName = userInfo.UserName;
            _oldEmail = userInfo.Email;
        }

        protected override async Task HandleEntity()
        {
            var user = new RequestEditUserInfoUserApiModel
            {
                UserName = UserName,
                Email = Email
            };
            ResponseEditUserInfoUserApiModel response = await _userService.EditUserInfo(user);
            if (response.IsSuccess)
            {
                _dialogsHelper.DisplayToastMessage(response.Message);
                await _navigationService.Close(this);
            }
        }

        private async Task DeleteUser()
        {
            bool isConfirmedToDelete = await _dialogsHelper.IsConfirmed(Strings.DeleteMessageDialog);

            if (!isConfirmedToDelete)
            {
                return;
            }

            DeleteUserApiModel response = await _userService.Delete<DeleteUserApiModel>();
            if (response.IsSuccess)
            {
                _storage.Clear();
                await _navigationService.Navigate<LoginViewModel>();
            }            
        }

        private async Task EditPassword()
        {
            await _navigationService.Navigate<EditPasswordViewModel>();
        }
    }
}
