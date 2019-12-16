using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.ApiModels.User;
using TestProject.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public RegistrationViewModel(IMvxNavigationService navigationService, IUserService userService)
            : base(navigationService)
        {
            _userService = userService;
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

        private async Task RegisterUser()
        {
            var user = new RequestRegisterUserApiModel
            {
                UserName = UserName,
                Password = Password,
                Email = Email
            };

            ResponseRegisterUserApiModel response = await _userService.RegisterUser(user);

            if (response.IsSuccess)
            {
                await _navigationService.Close(this);
            }
        }

    }
}
