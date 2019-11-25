using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using TestProject.ApiModels.User;
using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private GetProfileImageUserApiModel _currentUser;

        private readonly IUserRepository _userRepository;

        private readonly IDialogsHelper _dialogsHelper;

        private readonly IPhotoEditHelper _photoEditHelper;

        private readonly IUserService _userService;

        public MenuViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage, IUserService userService,
            IUserRepository userRepository, IDialogsHelper dialogsHelper, IPhotoEditHelper photoEditHelper)
            : base(navigationService, storage)
        {
            _userService = userService;
            _userRepository = userRepository;
            _dialogsHelper = dialogsHelper;
            _photoEditHelper = photoEditHelper;

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

            int userId = await _storage.Get();
            _currentUser = await _userService.GetUserWithImage(userId);
            ImageBytes = _currentUser.ImageBytes;
            UserName = _currentUser.Name;
        }

        private async Task Logout()
        {
            _storage.Clear();
            await _navigationService.Close(this);
            await _navigationService.Navigate<LoginViewModel>();
        }

        private async Task EditProfilePhoto()
        {
            //await _userService.EditProfilePhoto(_currentUser);

            ImageBytes = _currentUser.ImageBytes;
            //await _userRepository.Update(_currentUser);
        }

        private async Task ShowSettings()
        {
            await _navigationService.Navigate<UserSettingsViewModel>();
        }
    }
}
