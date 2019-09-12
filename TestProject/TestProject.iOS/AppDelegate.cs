using Foundation;
using UIKit;
using MvvmCross.Platforms.Ios.Core;
using TestProject.Core;

namespace TestProject.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {

        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);

            UINavigationBar.Appearance.Translucent = false;

            return result;
        }
    }
}


