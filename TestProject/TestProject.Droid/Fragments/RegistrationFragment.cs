using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using TestProject.Configurations;
using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.Droid.Fragments
{
    [Register("testProject.droid.views.RegistrationFragment")]
    public class RegistrationFragment : BaseFragment<RegistrationViewModel>
    {
        protected override int FragmentId => Resource.Layout.RegistrationFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var tvPasswordTip = view.FindViewById<TextView>(Resource.Id.tvPasswordTip);
            tvPasswordTip.Text =
                $"{Strings.PasswordTipFirst} {Constants.MinPasswordLength} {Strings.PasswordTipSecond}";

            return view;
        }
    }
}