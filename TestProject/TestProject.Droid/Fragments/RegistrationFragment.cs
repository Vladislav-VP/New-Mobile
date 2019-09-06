using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;

using TestProject.Configurations;
using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.Droid.Fragments
{
    [MvxFragmentPresentation(AddToBackStack = true)]
    [Register("testProject.droid.fragments.RegistrationFragment")]
    public class RegistrationFragment : BaseFragment<RegistrationViewModel>
    {
        protected override int FragmentId => Resource.Layout.RegistrationFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            InitializeAllControls(view);

            return view;
        }

        protected override void InitializeAllControls(View view)
        {
            TextView tvUsername = view.FindViewById<TextView>(Resource.Id.tvUsername);
            TextView tvPassword = view.FindViewById<TextView>(Resource.Id.tvPassword);
            TextView tvPasswordTip = view.FindViewById<TextView>(Resource.Id.tvPasswordTip);
            TextView tvRegistration = view.FindViewById<TextView>(Resource.Id.tvRegistration);
            Button btRegister = view.FindViewById<Button>(Resource.Id.btRegister);

            _controlSigningHelper.SignControl(tvUsername, Strings.UsernameTextViewLabel);
            _controlSigningHelper.SignControl(tvPassword, Strings.PasswordTextViewLabel);
            _controlSigningHelper.SignControl(tvRegistration, Strings.RegistrationTitle);
            _controlSigningHelper.SignControl(btRegister, Strings.RegistrationButtonLabel);

            string tvPasswordTipLabel =
                $"{Strings.PasswordTipFirst} {Constants.MinPasswordLength} {Strings.PasswordTipSecond}";
            _controlSigningHelper.SignControl(tvPasswordTip, tvPasswordTipLabel);
        }
    }
}