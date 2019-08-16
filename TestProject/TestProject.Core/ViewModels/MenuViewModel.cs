using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using System.Threading.Tasks;
using TestProject.Services;
using TestProject.Entities;
using TestProject.Configurations;
using TestProject.Services.Helpers;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Resources;
using MvvmCross.Plugin.PictureChooser;
using TestProject.Services.Repositories.Interfaces;
using System.IO;

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

        private readonly IMvxPictureChooserTask _pictureChooserTask;

        public MenuViewModel(IMvxNavigationService navigationService, IStorageHelper<User> storage,
            IUserRepository userRepository, IDialogsHelper dialogsHelper, IMvxPictureChooserTask pictureChooserTask)
            : base(navigationService, storage)
        {
            _userRepository = userRepository;
            _dialogsHelper = dialogsHelper;
            _pictureChooserTask = pictureChooserTask;

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
            Stream imageStream = null;

            switch (result)
            {
                case EditPhotoDialogResult.ChooseFromGallery:
                    imageStream = 
                        await _pictureChooserTask.ChoosePictureFromLibraryAsync(_maxPixelDimension, _percentQuality);
                    break;
                case EditPhotoDialogResult.TakePicture:
                    imageStream = await _pictureChooserTask.TakePictureAsync(_maxPixelDimension, _percentQuality);
                    break;
                case EditPhotoDialogResult.DeletePicture:
                    ProfilePhotoInfo = null;
                    break;
                default:
                    return;
            }

            if (imageStream != null)
            {
                UpdateProfilePhoto(imageStream);
                imageStream.Close();
            }

            _currentUser.ProfilePhotoInfo = ProfilePhotoInfo;
            await _userRepository.Update(_currentUser);
            await _navigationService.Navigate<MenuViewModel>();
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
