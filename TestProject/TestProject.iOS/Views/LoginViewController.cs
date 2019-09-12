using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.iOS.Views
{
    public partial class LoginViewController : MvxViewController<LoginViewModel>
    {
        public LoginViewController() : base(nameof(LoginViewController), null)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationController.SetNavigationBarHidden(true, true);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            lbUsername.Text = Strings.UsernameTextViewLabel;
            lbPassword.Text = Strings.PasswordTextViewLabel;
            btLogin.SetTitle(Strings.LoginButtonLabel, UIControlState.Normal);
            tfPassword.SecureTextEntry = true;
            lbWithoutAccount.Text = Strings.WithoutAccountPrompt;
            btRegistration.SetTitle(Strings.RegistrationButtonLabel, UIControlState.Normal);

            var set = this.CreateBindingSet<LoginViewController, LoginViewModel>();

            set.Bind(tfUsername).To(vm => vm.UserName);
            set.Bind(tfPassword).To(vm => vm.Password);
            set.Bind(btLogin).To(vm => vm.LoginCommand);
            set.Bind(btRegistration).To(vm => vm.GoToRegistrationCommand);

            set.Apply();
        }

        public override bool PrefersStatusBarHidden()
        {
            return true;
        }
    }
}