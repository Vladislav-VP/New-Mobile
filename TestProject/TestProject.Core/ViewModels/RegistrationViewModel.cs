using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using System.Threading.Tasks;
using TestProject.Entities;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Services.Repositories;
using TestProject.Services;
using TestProject.Configurations;
using Acr.UserDialogs;
using TestProject.Core.Resources;
using TestProject.Services.Helpers;
using Xamarin.Essentials;
using Plugin.SecureStorage;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Core.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;

        private readonly UserDialogsHelper _dialogsHelper;

        private string _userName;
        private string _password;

        public RegistrationViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            _userRepository = new UserRepository();

            _dialogsHelper = new UserDialogsHelper();

            RegistrateUserCommand = new MvxAsyncCommand(RegistrateUser);
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

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }
        public IMvxAsyncCommand RegistrateUserCommand { get; private set; }

        private async Task RegistrateUser()
        {
            User user = new User { Name = UserName, Password = Password };

            List<ValidationResult> validationResults;
            bool userIsValid = new DataValidationHelper().UserInfoIsValid(user, out validationResults);
            if (!userIsValid)
            {
                _dialogsHelper.ToastErrorMessage(validationResults[0].ErrorMessage);
                return;
            }

            if (await _userRepository.UserExists(UserName))
            {
                _dialogsHelper.ToastErrorMessage(Strings.UserAlreadyExistsMessage);
                return;
            }

            await AddUser();
            var users = await _userRepository.GetAllObjects<User>();
            await _navigationService.Navigate<TodoListItemViewModel>();
        }

        private async Task AddUser()
        {
            await _userRepository.Insert(new User { Name = UserName, Password = Password });

            await SaveUserIntoStorage();
        }

        private async Task SaveUserIntoStorage()
        {
            User user = await _userRepository.FindUser(UserName);
            await new CredentialsStorageHelper().Save(user);
        }
    }
}
