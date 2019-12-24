using Foundation;
using MvvmCross.Binding.BindingContext;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.iOS.Views
{
    public partial class LoginViewController : BaseViewController<LoginViewModel>
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationController.SetNavigationBarHidden(true, true);
        }

        public override void InitializeAllControls()
        {
            if (TabBarController != null)
            {
                TabBarController.TabBar.Hidden = true;
            }

            lbUsername.Text = Strings.UsernameTextViewLabel;
            lbPassword.Text = Strings.PasswordTextViewLabel;
            lbWithoutAccount.Text = Strings.WithoutAccountPrompt;
            
            tfPassword.SecureTextEntry = true;

            btLogin.SetTitle(Strings.LoginButtonLabel, UIControlState.Normal);
            btRegistration.SetTitle(Strings.RegistrationButtonLabel, UIControlState.Normal);
            tfUsername.ReturnKeyType = UIReturnKeyType.Next;
            tfPassword.ReturnKeyType = UIReturnKeyType.Done;

            tfUsername.ShouldReturn = (tf) =>
            {
                tfPassword.BecomeFirstResponder();
                return true;
            };
            tfPassword.ShouldReturn += ResignFirstResponder;
            lbForgotPassword.AttributedText =
                new NSAttributedString(Strings.ForgotPasswordLabel, underlineStyle : NSUnderlineStyle.Single);
        }

        public override void CreateBindings()
        {
            var set = this.CreateBindingSet<LoginViewController, LoginViewModel>();

            set.Bind(tfUsername).For(tf => tf.Text).To(vm => vm.UserName);
            set.Bind(tfPassword).For(tf => tf.Text).To(vm => vm.Password);
            set.Bind(btLogin).To(vm => vm.LoginCommand);
            set.Bind(btRegistration).To(vm => vm.GoToRegistrationCommand);
            UITapGestureRecognizer recognizer =
                new UITapGestureRecognizer(() => ViewModel.ForgotPasswordCommand.Execute(null));
            lbForgotPassword.AddGestureRecognizer(recognizer);

            set.Apply();
        }
    }
}