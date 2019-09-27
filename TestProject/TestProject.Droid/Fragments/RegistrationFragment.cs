using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;

using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.Droid.Fragments
{
    [MvxFragmentPresentation(ActivityHostViewModelType = typeof(LoginViewModel), AddToBackStack = true)]
    [Register("testProject.droid.fragments.RegistrationFragment")]
    public class RegistrationFragment : BaseFragment<RegistrationViewModel>
    {
        protected override int FragmentId => Resource.Layout.RegistrationFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            TextView tvUsername = view.FindViewById<TextView>(Resource.Id.tvUsername);
            TextView tvPassword = view.FindViewById<TextView>(Resource.Id.tvPassword);
            TextView tvPasswordTip = view.FindViewById<TextView>(Resource.Id.tvPasswordTip);
            TextView tvRegistration = view.FindViewById<TextView>(Resource.Id.tvRegistration);
            Button btRegister = view.FindViewById<Button>(Resource.Id.btRegister);

            tvUsername.Text = Strings.UsernameTextViewLabel;
            tvPassword.Text = Strings.PasswordTextViewLabel;
            tvRegistration.Text = Strings.RegistrationTitle;
            btRegister.Text = Strings.RegistrationButtonLabel;

            string tvPasswordTipLabel = Strings.PasswordTipLabel;
            tvPasswordTip.Text = tvPasswordTipLabel;

            return view;
        }
    }
}