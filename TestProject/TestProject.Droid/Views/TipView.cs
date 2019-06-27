using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Views;
using TestProject.Core.ViewModels;

namespace TestProject.Droid.Views
{
    [Activity(Label = "Tip Calculator", MainLauncher = true)]
    public class TipView : MvxActivity<TipViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.TipView);
        }
    }
}