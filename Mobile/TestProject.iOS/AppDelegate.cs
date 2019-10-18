using Foundation;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.Plugin.Color.Platforms.Ios;
using UIKit;

using TestProject.Core;
using TestProject.Resources;

namespace TestProject.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);

            CustomizeAppearance();

            return result;
        }

        private void CustomizeAppearance()
        {
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
            {
                TextColor = UIColor.White,
                Font = UIFont.SystemFontOfSize(Constants.MainFontSize, UIFontWeight.Semibold)
            });
            UINavigationBar.Appearance.Translucent = false;
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.BarTintColor = AppColors.MainInterfaceBlue.ToNativeColor();

            UITabBar.Appearance.BackgroundColor = UIColor.Blue;
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes()
            {
                TextColor = UIColor.Black
            }, UIControlState.Normal);

            UIStackView.Appearance.BackgroundColor = AppColors.MainInterfaceBlue.ToNativeColor();
            UIStackView.Appearance.TintColor = AppColors.MainInterfaceBlue.ToNativeColor();
        }
    }
}


