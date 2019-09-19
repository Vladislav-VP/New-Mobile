using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class RegistrationViewModel : UserViewModel
    {
        public RegistrationViewModel(IMvxNavigationService navigationService, IUserRepository userRepository, 
            IUserStorageHelper storage, IValidationHelper validationHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, storage, userRepository, validationHelper, dialogsHelper)
        {
            RegisterUserCommand = new MvxAsyncCommand(RegisterUser);
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

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public IMvxAsyncCommand RegisterUserCommand { get; private set; }

        protected override async Task<bool> IsDataValid()
        {
            User user = new User { Name = UserName, Password = Password };

            bool isUserDataValid = _validationHelper.IsObjectValid(user, nameof(user.Name))
                && _validationHelper.IsObjectValid(user, nameof(user.Password));
            if (!isUserDataValid)
            {
                return false;
            }

            user.Name = user.Name.Trim();
            User retrievedUser = await _userRepository.GetUser(user.Name);
            if (retrievedUser != null)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.UserAlreadyExistsMessage);
                return false;
            }

            return true;
        }

        private async Task RegisterUser()
        {
            bool isUserValid = await IsDataValid();
            if (!isUserValid)
            {
                return;
            }

            await AddUser();
            await _navigationService.Navigate<MainViewModel>();
        }

        private async Task AddUser()
        {
            User user = new User { Name = UserName, Password = Password };
            await _userRepository.Insert(user);
            await _storage.Save(user.Id);
        }
    }
}
