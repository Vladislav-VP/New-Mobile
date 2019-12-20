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
    [Register("testProject.droid.fragments.ResetPasswordFragment")]
    public class ResetPasswordFragment : BaseFragment<ResetPasswordViewModel>
    {
        protected override int FragmentId => Resource.Layout.ResetPasswordFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            TextView tvPasswordReset = view.FindViewById<TextView>(Resource.Id.tvPasswordReset);
            TextView tvEmailForRecovery = view.FindViewById<TextView>(Resource.Id.tvEmailForRecovery);
            Button btSend = view.FindViewById<Button>(Resource.Id.btSend);

            tvPasswordReset.Text = Strings.PasswordResetLabel;
            tvEmailForRecovery.Text = Strings.EmailForRecoveryLabel;
            btSend.Text = Strings.SendLabel;

            return view;
        }
    }
}
