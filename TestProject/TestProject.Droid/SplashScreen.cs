using Android.Content.PM;
using Android.App;
using MvvmCross.Platforms.Android.Views;

namespace TestProject.Droid
{
    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}