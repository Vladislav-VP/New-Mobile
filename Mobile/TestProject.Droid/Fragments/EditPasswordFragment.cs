using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.Droid.Fragments
{
    [Register("testProject.droid.fragments.EditPasswordFragment")]
    public class EditPasswordFragment : BaseDialogFragment<EditPasswordViewModel>
    {
        protected override int FragmentId => Resource.Layout.EditPasswordFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            TextView tvOldPassword = view.FindViewById<TextView>(Resource.Id.tvOldPassword);
            TextView tvNewPassword = view.FindViewById<TextView>(Resource.Id.tvNewPassword);
            TextView tvConfirmNewPassword = view.FindViewById<TextView>(Resource.Id.tvConfirmNewPassword);
            Button btSavePasssword = view.FindViewById<Button>(Resource.Id.btSavePasssword);

            tvOldPassword.Text = Strings.EnterOldPasswordTipLabel;
            tvNewPassword.Text = Strings.EnterNewPasswordTipLabel;
            tvConfirmNewPassword.Text = Strings.ConfirmNewPasswordTipLabel;
            btSavePasssword.Text = Strings.SaveChangesButtonLabel;

            return view;
        }
    }
}