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
        private bool IsStarted { get; set; } = false;

        private MenuOptionView _logoutOption;

        public MenuViewController() : base(nameof(MenuViewController), null)
        {
            _logoutOption = new MenuOptionView();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeAllControls();

            CreateBindings();
        }

        public void CreateBindings()
        {
            //    var source = new TableSource(tbviewMenuItems, Constants.MenuBindingText);
            //    var bindingMap=new Dictionary<object, string>()
            //    {

            //    }

            var set = this.CreateBindingSet<MenuViewController, MenuViewModel>();

            set.Bind(lbUsername).To(vm => vm.UserName);

            set.Bind(_logoutOption.Tap()).For(g => g.Command).To(vm => vm.LogoutCommand);
            set.Bind(imviewProfile.Tap()).For(g => g.Command).To(vm => vm.EditProfilePhotoCommand);

            set.Apply();
        }

        public void InitializeAllControls()
        {
            Title = Strings.MenuLabel;
            UINavigationBar.Appearance.BarTintColor = AppColors.MainInterfaceBlue.ToNativeColor();
            NavigationItem.SetHidesBackButton(false, false);

            CGSize profilePhotoSize = imviewProfile.Layer.PreferredFrameSize();
            imviewProfile.Layer.CornerRadius = profilePhotoSize.Height / 2;
            imviewProfile.Layer.MasksToBounds = true;

            _logoutOption.Label.Text = Strings.LogoutLabel;
            stviewMenuItems.AddArrangedSubview(_logoutOption);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }
    }
}