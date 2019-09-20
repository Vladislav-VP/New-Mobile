using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.iOS.Helpers.Interfaces;
using TestProject.Resources;
using System.Threading.Tasks;

namespace TestProject.iOS.Views
{
    [MvxModalPresentation(WrapInNavigationController = true, ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
    public partial class UserSettingsViewController : MvxViewController<UserSettingsViewModel>, IControlsSettingHelper
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeAllControls();

            CreateBindings();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override async void ViewWillAppear(bool animated)
        {
            await Task.Delay(3000);
            base.ViewWillAppear(animated);  
        }

        public void InitializeAllControls()
        {
            Title = Strings.SettingsLabel;

            NavigationItem.LeftBarButtonItem = new UIBarButtonItem();
            NavigationItem.LeftBarButtonItem.Title = Strings.BackLabel;
            NavigationItem.LeftBarButtonItem.TintColor = UIColor.White;
            NavigationItem.LeftBarButtonItem.Clicked += CancelClicked;

            lbUsername.Text = Strings.UsernameTextViewLabel;
            btChangePassword.SetTitle(Strings.ChangePasswordButtonLabel, UIControlState.Normal);
            btDeleteAccount.SetTitle(Strings.DeleteAccountButtonLabel, UIControlState.Normal);
            btSaveChanges.SetTitle(Strings.SaveChangesButtonLabel, UIControlState.Normal);
        }

        public void CreateBindings()
        {
            var set = this.CreateBindingSet<UserSettingsViewController, UserSettingsViewModel>();

            set.Bind(tfUsername).To(vm => vm.NewUserName);
            set.Bind(btSaveChanges).To(vm => vm.UpdateUserCommand);
            set.Bind(btChangePassword).To(vm => vm.EditPasswordCommand);
            set.Bind(btDeleteAccount).To(vm => vm.DeleteUserCommand);

            set.Apply();
        }

        private void CancelClicked(object sender, System.EventArgs e)
        {
            if (ViewModel.GoBackCommand != null)
            {
                ViewModel.GoBackCommand.Execute(null);
            }
        }

    }
}