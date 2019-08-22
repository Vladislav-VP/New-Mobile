using System;
using System.IO;
using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.Media;
using Plugin.Media.Abstractions;

using TestProject.Entities;
using TestProject.Services.Enums;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private User _currentUser;

        private readonly IPermissionsHelper _permissionsHelper;

        private readonly IUserRepository _userRepository;

        private readonly IUserDialogsHelper _dialogsHelper;

        private readonly IPhotoCaptureHelper _photoHelper;
        
        public MenuViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage, IPhotoCaptureHelper photoHelper,
            IUserRepository userRepository, IUserDialogsHelper dialogsHelper, IPermissionsHelper permissionsHelper)
            : base(navigationService, storage)
        {
            _userRepository = userRepository;
            _dialogsHelper = dialogsHelper;
            _permissionsHelper = permissionsHelper;
            _photoHelper = photoHelper;

            ShowLoginViewModelCommand = new MvxAsyncCommand(Logout);
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

        public IMvxAsyncCommand ShowLoginViewModelCommand { get; private set; }

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
            EditPhotoDialogResult result = await _dialogsHelper.ChooseOption();
            await HandleChangePhotoResult(result);
        }

        private async Task HandleChangePhotoResult(EditPhotoDialogResult result)
        {
            Stream imageStream = await GetImageStream(result);

            if (imageStream != null)
            {
                EncryptedProfilePhoto = GetEncryptedString(imageStream);
                imageStream.Close();
            }

            _currentUser.EncryptedProfilePhoto = EncryptedProfilePhoto;
            await _userRepository.Update(_currentUser);
            // TODO: Correcrt issue with navigation to menu (profile photo is not updated after being deleted).
            await _navigationService.Navigate<MenuViewModel>();
        }

        private async Task<Stream> GetImageStream(EditPhotoDialogResult result)
        {
            Stream imageStream = null;

            switch (result)
            {
                case EditPhotoDialogResult.ChooseFromGallery:
                    MediaFile pickedPhoto = await _photoHelper.PickPhoto();
                    imageStream = pickedPhoto.GetStream();
                    break;
                case EditPhotoDialogResult.TakePicture:
                    bool arePermissionsGranted = await _permissionsHelper.TryRequestPermissions();
                    if (arePermissionsGranted)
                    {
                        MediaFile takenPhoto = await _photoHelper.TakePhoto();
                        imageStream = takenPhoto.GetStream();
                    }
                    break;
                case EditPhotoDialogResult.DeletePicture:
                    EncryptedProfilePhoto = null;
                    break;
            }

            return imageStream;
        }
        
        private string GetEncryptedString(Stream imageStream)
        {
            string encryptedString = null;

            using (var memoryStream = new MemoryStream())
            {
                imageStream.CopyTo(memoryStream);
                byte[] decryptedImageString = memoryStream.ToArray();
                encryptedString = Convert.ToBase64String(decryptedImageString);
            }

            return encryptedString;
        }
    }
}
