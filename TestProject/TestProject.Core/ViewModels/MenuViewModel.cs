using System;
using System.IO;
using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

using TestProject.Entities;
using TestProject.Services.Helpers;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private const int _maxPixelDimension = 200;
        private const int _percentQuality = 90;

        private User _currentUser;
        private string _userName;
        private string _profilePhotoInfo;

        private readonly IUserRepository _userRepository;

        private readonly IDialogsHelper _dialogsHelper;
        
        public MenuViewModel(IMvxNavigationService navigationService, IStorageHelper<User> storage,
            IUserRepository userRepository, IDialogsHelper dialogsHelper)
            : base(navigationService, storage)
        {
            _userRepository = userRepository;
            _dialogsHelper = dialogsHelper;

            ShowLoginViewModelCommand = new MvxAsyncCommand(Logout);
            ShowUserInfoViewModelCommand = new MvxAsyncCommand(async 
                () => await _navigationService.Navigate<UserSettingsViewModel>());
            ShowListTodoItemsViewModelCommand = 
                new MvxAsyncCommand(async () => await _navigationService.Navigate<TodoListItemViewModel>());
            EditProfilePhotoCommand = new MvxAsyncCommand(EditProfilePhoto);
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

        public string ProfilePhotoInfo
        {
            get => _profilePhotoInfo;
            set
            {
                _profilePhotoInfo = value;
                RaisePropertyChanged(() => ProfilePhotoInfo);
            }
        }

        public IMvxAsyncCommand ShowLoginViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowUserInfoViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowListTodoItemsViewModelCommand { get; private set; }

        public IMvxAsyncCommand EditProfilePhotoCommand { get; private set; }

        public async override Task Initialize()
        {
            await base.Initialize();

            _currentUser = await _storage.Retrieve();
            UserName = _currentUser.Name;
            ProfilePhotoInfo = _currentUser.ProfilePhotoInfo;
        }

        private async Task Logout()
        {
            _storage.Clear();
            var result = await _navigationService.Navigate<LoginViewModel>();
        }

        private async Task EditProfilePhoto()
        {
            var result = await _dialogsHelper.ChooseOption();
            await HandleChangePhotoResult(result);
        }

        private async Task HandleChangePhotoResult(EditPhotoDialogResult result)
        {
            Stream imageStream = await GetImageStream(result);

            if (imageStream != null)
            {
                UpdateProfilePhoto(imageStream);
                imageStream.Close();
            }

            _currentUser.ProfilePhotoInfo = ProfilePhotoInfo;
            await _userRepository.Update(_currentUser);
            await _navigationService.Navigate<MenuViewModel>();
        }

        private async Task<Stream> GetImageStream(EditPhotoDialogResult result)
        {
            Stream imageStream = null;

            switch (result)
            {
                case EditPhotoDialogResult.ChooseFromGallery:
                    var pickedPhoto = await PickPhoto();
                    imageStream = pickedPhoto.GetStream();
                    break;
                case EditPhotoDialogResult.TakePicture:
                    bool permissionsAreGranted = await TryRequestPermissions();
                    if (permissionsAreGranted)
                    {
                        var takenPhoto = await TakePhoto();
                        imageStream = takenPhoto.GetStream();
                    }
                    break;
                case EditPhotoDialogResult.DeletePicture:
                    ProfilePhotoInfo = null;
                    break;
            }

            return imageStream;
        }
        
        private async Task<MediaFile> PickPhoto()
        {
            var options = new PickMediaOptions
            {
                CompressionQuality = _percentQuality,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = _maxPixelDimension
            };

            return await CrossMedia.Current.PickPhotoAsync(options);
        }

        private async Task<MediaFile> TakePhoto()
        {
            var options = new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                CompressionQuality = _percentQuality,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = _maxPixelDimension,
                DefaultCamera = CameraDevice.Rear
            };

            return await CrossMedia.Current.TakePhotoAsync(options);
        }

        private async Task<bool> TryRequestPermissions()
        {
            bool isInitialized = await CrossMedia.Current.Initialize();
            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var permissionsDictionary = await CrossPermissions
                    .Current
                    .RequestPermissionsAsync(Permission.Camera, Permission.Storage);
                cameraStatus = permissionsDictionary[Permission.Camera];
                storageStatus = permissionsDictionary[Permission.Storage];
            }

            return cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted;
        }

        private void UpdateProfilePhoto(Stream imageStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                imageStream.CopyTo(memoryStream);
                byte[] decryptedImageString = memoryStream.ToArray();
                ProfilePhotoInfo = Convert.ToBase64String(decryptedImageString);
            }
        }
    }
}
