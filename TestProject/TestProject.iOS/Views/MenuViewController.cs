using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TestProject.Core.ViewModels;
using TestProject.iOS.CustomControls;
using TestProject.iOS.Helpers.Interfaces;
using TestProject.Resources;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using UIKit;
using MvvmCross.Plugin.Color.Platforms.Ios;

namespace TestProject.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Menu")]
    public partial class MenuViewController : MvxViewController<MenuViewModel>, IControlsSettingHelper
    {
        private MenuOptionView _logoutOption;
        private MenuOptionView _settingsOption;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeAllControls();

            CreateBindings();
        }

        public void CreateBindings()
        {
            var set = this.CreateBindingSet<MenuViewController, MenuViewModel>();

            set.Bind(lbUsername).To(vm => vm.UserName);

            set.Bind(_logoutOption.Tap()).For(g => g.Command).To(vm => vm.LogoutCommand);
            set.Bind(_settingsOption.Tap()).For(g => g.Command).To(vm => vm.ShowSettingsCommand);
            set.Bind(imviewProfile.Tap()).For(g => g.Command).To(vm => vm.EditProfilePhotoCommand);

            set.Apply();
        }

        public void InitializeAllControls()
        {
            Title = Strings.MenuLabel;
            UINavigationBar.Appearance.BarTintColor = AppColors.MainInterfaceBlue.ToNativeColor();
            NavigationItem.SetHidesBackButton(false, false);

            _logoutOption = new MenuOptionView();
            _settingsOption = new MenuOptionView();

            CGSize profilePhotoSize = imviewProfile.Layer.PreferredFrameSize();
            imviewProfile.Layer.CornerRadius = profilePhotoSize.Height / 2;
            imviewProfile.Layer.MasksToBounds = true;

            _logoutOption.Label.Text = Strings.LogoutLabel;
            _settingsOption.Label.Text = Strings.SettingsLabel;

            stviewMenuItems.AddArrangedSubview(_settingsOption);
            stviewMenuItems.AddArrangedSubview(_logoutOption);
        }
    }
}