using MvvmCross.Binding.BindingContext;
using MvvmCross.Plugin.Color.Platforms.Ios;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.Resources;
using MvvmCross.Platforms.Ios.Presenters.Attributes;

namespace TestProject.iOS.Views
{
    [MvxChildPresentation]
    public partial class ResetPasswordViewController : BaseViewController<ResetPasswordViewModel>
    {
        public override void InitializeAllControls()
        {
            Title = Strings.PasswordResetLabel;
            NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
            UINavigationBar.Appearance.BarTintColor = AppColors.DarkBlue.ToNativeColor();
            NavigationController.SetNavigationBarHidden(false, true);

            lbRecoveryEmail.Text = Strings.EmailForRecoveryLabel;
            btSend.SetTitle(Strings.SendLabel, UIControlState.Normal);

            tfRecoveryEmail.ReturnKeyType = UIReturnKeyType.Done;

            tfRecoveryEmail.ShouldReturn += ResignFirstResponder;
        }

        public override void CreateBindings()
        {
            var set = this.CreateBindingSet<ResetPasswordViewController, ResetPasswordViewModel>();
            
            set.Bind(tfRecoveryEmail).To(vm => vm.Email);
            set.Bind(btSend).To(vm => vm.SendRecoveryMailCommand);

            set.Apply();
        }
    }
}