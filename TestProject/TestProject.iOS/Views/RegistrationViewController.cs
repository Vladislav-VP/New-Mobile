using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Color.Platforms.Ios;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.iOS.Helpers.Interfaces;
using TestProject.Resources;

namespace TestProject.iOS.Views
{
    public partial class RegistrationViewController : MvxViewController<RegistrationViewModel>, IControlsSettingHelper
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeAllControls();

            CreateBindings();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ViewModel.GoBackCommand.Execute(null);
        }

        public void InitializeAllControls()
        {
            Title = Strings.RegistrationTitle;
            NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
            UINavigationBar.Appearance.BarTintColor = AppColors.DarkBlue.ToNativeColor();
            NavigationController.SetNavigationBarHidden(false, true);

            lbUsername.Text = Strings.UsernameTextViewLabel;
            lbPassword.Text = Strings.PasswordTextViewLabel;
            btRegistration.SetTitle(Strings.RegistrationButtonLabel, UIControlState.Normal);
            tfPassword.SecureTextEntry = true;
        }

        public void CreateBindings()
        {
            var set = this.CreateBindingSet<RegistrationViewController, RegistrationViewModel>();

            set.Bind(tfUsername).To(vm => vm.UserName);
            set.Bind(tfPassword).To(vm => vm.Password);
            set.Bind(btRegistration).To(vm => vm.RegisterUserCommand);

            set.Apply();
        }
    }
}