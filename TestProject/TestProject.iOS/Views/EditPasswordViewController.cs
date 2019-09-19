using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.iOS.Helpers.Interfaces;
using TestProject.Resources;

namespace TestProject.iOS.Views
{
    [MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen)]
    public partial class EditPasswordViewController : MvxViewController<EditPasswordViewModel>, IControlsSettingHelper
    {        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeAllControls();

            CreateBindings();
        }

        public void InitializeAllControls()
        {
            lbOldPassword.Text = Strings.EnterOldPasswordTipLabel;
            lbNewPassword.Text = Strings.EnterNewPasswordTipLabel;
            lbPasswordConfirmation.Text = Strings.ConfirmNewPasswordTipLabel;

            tfOldPassword.SecureTextEntry = true;
            tfNewPassword.SecureTextEntry = true;
            tfPasswordConfirmation.SecureTextEntry = true;

            btSaveChanges.SetTitle(Strings.SaveChangesButtonLabel, UIControlState.Normal);
            btCancel.SetTitle(Strings.CancelText, UIControlState.Normal);
        }

        public void CreateBindings()
        {
            var set = this.CreateBindingSet<EditPasswordViewController, EditPasswordViewModel>();

            set.Bind(tfOldPassword).To(vm => vm.OldPassword);
            set.Bind(tfNewPassword).To(vm => vm.NewPassword);
            set.Bind(tfPasswordConfirmation).To(vm => vm.NewPasswordConfirmation);
            set.Bind(btSaveChanges).To(vm => vm.ChangePasswordCommand);
            set.Bind(btCancel).To(vm => vm.GoBackCommand);

            set.Apply();
        }
    }
}