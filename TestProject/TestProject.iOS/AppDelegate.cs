using Foundation;
using UIKit;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.Plugin.Color.Platforms.Ios;
using TestProject.Core;
using TestProject.Resources;
using MvvmCross.UI;

namespace TestProject.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {

        public override UIWindow Window { get; set; }

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
                Font = UIFont.SystemFontOfSize(30f, UIFontWeight.Semibold)
            });
            UINavigationBar.Appearance.Translucent = false;
            UINavigationBar.Appearance.TintColor = UIColor.White;

            UITabBar.Appearance.BackgroundColor = AppColors.PrimaryColor.ToNativeColor();
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes()
            {
                TextColor = AppColors.AccentColor.ToNativeColor()
            }, UIControlState.Selected);



            UITextView.Appearance.TintColor = AppColors.AccentColor.ToNativeColor();
            UIButton.Appearance.SetTitleColor(AppColors.AccentColor.ToNativeColor(), UIControlState.Highlighted);
        }
    }
}


