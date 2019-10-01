using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.iOS.Views
{
    [MvxModalPresentation(WrapInNavigationController = true, ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
    public partial class UserSettingsViewController : BaseEntityViewController
    {
        public new UserSettingsViewModel ViewModel
        {
            get => (UserSettingsViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        public override void InitializeAllControls()
        {
            base.InitializeAllControls();

            Title = Strings.SettingsLabel;

            lbUsername.Text = Strings.UsernameTextViewLabel;
            btChangePassword.SetTitle(Strings.ChangePasswordButtonLabel, UIControlState.Normal);
            btDeleteAccount.SetTitle(Strings.DeleteAccountButtonLabel, UIControlState.Normal);
            btSaveChanges.SetTitle(Strings.SaveChangesButtonLabel, UIControlState.Normal);
        }

        public override void CreateBindings()
        {
            var set = this.CreateBindingSet<UserSettingsViewController, UserSettingsViewModel>();

            set.Bind(tfUsername).To(vm => vm.UserName);
            set.Bind(btSaveChanges).To(vm => vm.UpdateUserCommand);
            set.Bind(btChangePassword).To(vm => vm.EditPasswordCommand);
            set.Bind(btDeleteAccount).To(vm => vm.DeleteUserCommand);

            set.Apply();
        }
    }
}