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
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            InitializeAllControls(view);

            return view;
        }

        protected override void InitializeAllControls(View view)
        {
            TextView tvUsername = view.FindViewById<TextView>(Resource.Id.tvUsername);
            TextView tvPassword = view.FindViewById<TextView>(Resource.Id.tvPassword);
            TextView tvPasswordTip = view.FindViewById<TextView>(Resource.Id.tvPasswordTip);
            Button btRegister = view.FindViewById<Button>(Resource.Id.btRegister);

            _controlSigningHelper.SignControl(tvUsername, ControlsLabels.UsernameTextViewLabel);
            _controlSigningHelper.SignControl(tvPassword, ControlsLabels.PasswordTextViewLabel);
            _controlSigningHelper.SignControl(btRegister, ControlsLabels.RegistrationButtonLabel);
            string tvPasswordTipLabel =
                $"{Strings.PasswordTipFirst} {Constants.MinPasswordLength} {Strings.PasswordTipSecond}";
            _controlSigningHelper.SignControl(tvPasswordTip, tvPasswordTipLabel);
        }
    }
}