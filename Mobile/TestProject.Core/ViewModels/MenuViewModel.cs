using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private User _currentUser;

        private readonly IUserRepository _userRepository;

        private readonly IDialogsHelper _dialogsHelper;

        private readonly IPhotoEditHelper _photoEditHelper;

        private readonly IUserService _userService;

        public MenuViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage, IUserService userService,
            IUserRepository userRepository, IDialogsHelper dialogsHelper, IPhotoEditHelper photoEditHelper)
            : base(navigationService, storage)
        {
            _userRepository = userRepository;
            _dialogsHelper = dialogsHelper;
            _photoEditHelper = photoEditHelper;
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

        public string EncryptedProfilePhoto
        {
            get => _currentUser.ImageUrl;
            set
            {
                _currentUser.ImageUrl = value;
                RaisePropertyChanged(() => EncryptedProfilePhoto);
            }
        }

        public IMvxAsyncCommand LogoutCommand { get; private set; }

        public IMvxAsyncCommand ShowSettingsCommand { get; private set; }

        public IMvxAsyncCommand ShowTodoItemListCommand { get; private set; }

        public IMvxAsyncCommand EditProfilePhotoCommand { get; private set; }

        public async override Task Initialize()
        {
            await base.Initialize();

            int userId = await _storage.Get();
            _currentUser = await _userService.Get(userId);

            UserName = _currentUser.Name;
            EncryptedProfilePhoto = _currentUser.ImageUrl;
        }

        private async Task Logout()
        {
            _storage.Clear();
            await _navigationService.Close(this);
            await _navigationService.Navigate<LoginViewModel>();
        }

        private async Task ChangeProfilePhoto()
        {
            string[] buttons =
                {
                    Strings.ChoosePicture,
                    Strings.TakePicture
                };

            Dictionary<string, Func<Task<string>>> optionResultPairs =
                new Dictionary<string, Func<Task<string>>>();
            optionResultPairs.Add(Strings.CancelText, null);
            optionResultPairs.Add(Strings.ChoosePicture, _photoEditHelper.PickPhoto);
            optionResultPairs.Add(Strings.TakePicture, _photoEditHelper.TakePhoto);
            optionResultPairs.Add(Strings.DeletePicture, _photoEditHelper.DeletePhoto);

            string option = await _dialogsHelper.ChooseOption(Strings.ProfilePhotoTitle,
                Strings.CancelText, Strings.DeletePicture, buttons: buttons);

            if (option != Strings.CancelText)
            {
                EncryptedProfilePhoto = await optionResultPairs[option]();
            }
        }

        private async Task EditProfilePhoto()
        {
            await ChangeProfilePhoto();

            await _userRepository.Update(_currentUser);
        }

        private async Task ShowSettings()
        {
            await _navigationService.Navigate<UserSettingsViewModel>();
        }
    }
}
