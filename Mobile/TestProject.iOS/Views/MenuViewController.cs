using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;

using TestProject.Core.ViewModels;
using TestProject.iOS.CustomControls;
using TestProject.Resources;

namespace TestProject.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Menu", TabIconName = "ic_menu")]
    public partial class MenuViewController : BaseViewController<MenuViewModel>
    {
        private MenuOptionView _logoutOption;
        private MenuOptionView _settingsOption;

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            await ViewModel.Initialize();
        }

        public override void CreateBindings()
        {
            var set = this.CreateBindingSet<MenuViewController, MenuViewModel>();

            set.Bind(lbUsername).To(vm => vm.UserName);

            set.Bind(_logoutOption.Tap()).For(g => g.Command).To(vm => vm.LogoutCommand);
            set.Bind(_settingsOption.Tap()).For(g => g.Command).To(vm => vm.ShowSettingsCommand);
            set.Bind(imviewProfile.Tap()).For(g => g.Command).To(vm => vm.EditProfilePhotoCommand);

            set.Bind(imviewProfile)
                .For(v => v.Image)
                .To(vm => vm.EncryptedProfilePhoto)
                .WithConversion("ImageValue");

            set.Apply();
        }

        public override void InitializeAllControls()
        {
            Title = Strings.MenuLabel;
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