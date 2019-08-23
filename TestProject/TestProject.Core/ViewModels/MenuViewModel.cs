using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Services.Enums;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private User _currentUser;

        private readonly IUserRepository _userRepository;

        private readonly IUserDialogsHelper _userDialogsHelper;

        private readonly IPhotoEditHelper _photoEditHelper;
        
        public MenuViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage,
            IUserRepository userRepository, IUserDialogsHelper userDialogsHelper, IPhotoEditHelper photoEditHelper)
            : base(navigationService, storage)
        {
            _userRepository = userRepository;
            _userDialogsHelper = userDialogsHelper;
            _photoEditHelper = photoEditHelper;

            LogoutCommand = new MvxAsyncCommand(Logout);
            ShowUserInfoViewModelCommand = new MvxAsyncCommand(async 
                () => await _navigationService.Navigate<UserSettingsViewModel>());
            ShowListTodoItemsViewModelCommand = 
                new MvxAsyncCommand(async () => await _navigationService.Navigate<TodoListItemViewModel>());
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

        private string _encryptedProfilePhoto;
        public string EncryptedProfilePhoto
        {
            get => _encryptedProfilePhoto;
            set
            {
                _encryptedProfilePhoto = value;
                RaisePropertyChanged(() => EncryptedProfilePhoto);
            }
        }

        public IMvxAsyncCommand LogoutCommand { get; private set; }

        public IMvxAsyncCommand ShowUserInfoViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowListTodoItemsViewModelCommand { get; private set; }

        public IMvxAsyncCommand EditProfilePhotoCommand { get; private set; }

        public async override Task Initialize()
        {
            await base.Initialize();

            _currentUser = await _storage.Get();
            UserName = _currentUser.Name;
            EncryptedProfilePhoto = _currentUser.EncryptedProfilePhoto;
        }

        private async Task Logout()
        {
            _storage.Clear();
            await _navigationService.Close(this);
            await _navigationService.Navigate<LoginViewModel>();
        }

        private async Task EditProfilePhoto()
        {
            EditPhotoDialogResult result = await _userDialogsHelper.ChoosePhotoEditOption();

            if (result == EditPhotoDialogResult.Cancel)
            {
                return;
            }

            _currentUser.EncryptedProfilePhoto = await _photoEditHelper.ReplacePhoto(result);

            await _userRepository.Update(_currentUser);
            await _navigationService.Navigate<MenuViewModel>();
        }
    }
}
