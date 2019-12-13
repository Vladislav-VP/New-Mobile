using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.ApiModels.User;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private GetProfileImageUserApiModel _currentUser;

        private readonly IUserService _userService;

        public MenuViewModel(IMvxNavigationService navigationService, IStorageHelper storage, IUserService userService, IDialogsHelper dialogsHelper, IPhotoEditHelper photoEditHelper)
            : base(navigationService, storage)
        {
            _userService = userService;

            LogoutCommand = new MvxAsyncCommand(Logout);
            ShowSettingsCommand = new MvxAsyncCommand(ShowSettings);
            ShowTodoItemListCommand =
                new MvxAsyncCommand(async () => await _navigationService.Navigate<TodoItemListViewModel>());
            EditProfilePhotoCommand = new MvxAsyncCommand(EditProfilePhoto);
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

        public byte[] ImageBytes
        {
            get => _currentUser.ImageBytes;
            set
            {
                _currentUser.ImageBytes = value;
                RaisePropertyChanged(() => ImageBytes);
            }
        }

        public IMvxAsyncCommand LogoutCommand { get; private set; }

        public IMvxAsyncCommand ShowSettingsCommand { get; private set; }

        public IMvxAsyncCommand ShowTodoItemListCommand { get; private set; }

        public IMvxAsyncCommand EditProfilePhotoCommand { get; private set; }

        public async override Task Initialize()
        {
            await base.Initialize();

            _currentUser = await _userService.GetUserWithImage();
            ImageBytes = _currentUser.ImageBytes;
            UserName = _currentUser.UserName;
        }

        private async Task Logout()
        {
            await _userService.Logout();
            await _navigationService.Close(this);
            await _navigationService.Navigate<LoginViewModel>();
        }

        private async Task EditProfilePhoto()
        {
            var user = new RequestEditProfileImageUserApiModel
            {
                ImageBytes = ImageBytes
            };
            ResponseEditProfileImageUserApiModel response = await _userService.EditProfilePhoto(user);
            ImageBytes = response.ImageBytes;
        }

        private async Task ShowSettings()
        {
            await _navigationService.Navigate<UserSettingsViewModel>();
        }
    }
}
