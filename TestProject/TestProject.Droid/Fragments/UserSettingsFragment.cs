using Android.Runtime;
using MvvmCross.Platforms.Android.Presenters.Attributes;

using TestProject.Core.ViewModels;

namespace TestProject.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("testProject.droid.views.UserInfoView")]
    public class UserSettingsFragment : BaseFragment<UserSettingsViewModel>
    {
        protected override int FragmentId => Resource.Layout.UserSettingsFragment;
    }
}