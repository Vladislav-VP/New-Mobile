using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;
        
        public LoginViewModel(IMvxNavigationService navigationService, ILoginService loginService)
            : base(navigationService)
        {
            _loginService = loginService;

            LoginCommand = new MvxAsyncCommand(Login);
            GoToRegistrationCommand = new MvxAsyncCommand(GoToRegistration);
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

        public IMvxAsyncCommand LoginCommand { get; private set; }
        
        public IMvxAsyncCommand GoToRegistrationCommand { get; private set; }        

        private async Task Login()
        {
            var user = new User { Name = UserName, Password = Password };
            var result = new LoginResult { Data = user };
            await _loginService.Login(result);

            if (result.IsSucceded)
            {
                await _navigationService.Navigate<MainViewModel>();
            }            
        }

        private async Task GoToRegistration()
        {
            await _navigationService.Navigate<RegistrationViewModel>();
        }
    }
}
