using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.iOS.Helpers.Interfaces;
using TestProject.Resources;
using MvvmCross.Platforms.Ios.Presenters.Attributes;

namespace TestProject.iOS.Views
{
    //[MvxTabPresentation(WrapInNavigationController = true)]
    public partial class UserSettingsViewController : MvxViewController<UserSettingsViewModel>, IControlsSettingHelper
    {
        public UserSettingsViewController() : base(nameof(UserSettingsViewController), null)
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

            InitializeAllControls();

            CreateBindings();
        }

        public override bool PrefersStatusBarHidden()
        {
            return true;
        }

        public void InitializeAllControls()
        {

        }

        public void CreateBindings()
        {
            var set = this.CreateBindingSet<UserSettingsViewController, UserSettingsViewModel>();

            set.Apply();
        }
    }
}