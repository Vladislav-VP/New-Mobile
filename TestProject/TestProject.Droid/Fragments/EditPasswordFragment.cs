using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding.BindingContext;

using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.Droid.Fragments
{
    [Register("testProject.droid.views.EditPasswordFragment")]
    public class EditPasswordFragment : BaseDialogFragment<EditPasswordViewModel>
    {
        protected override int FragmentId => Resource.Layout.EditPasswordFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View ignore = base.OnCreateView(inflater, container, savedInstanceState);

            View view = this.BindingInflate(FragmentId, null);

            InitializeAllControls(view);

            return view;
        }

        protected override void InitializeAllControls(View view)
        {
            TextView tvOldPassword = view.FindViewById<TextView>(Resource.Id.tvOldPassword);
            TextView tvNewPassword = view.FindViewById<TextView>(Resource.Id.tvNewPassword);
            TextView tvConfirmNewPassword = view.FindViewById<TextView>(Resource.Id.tvConfirmNewPassword);
            Button btSavePasssword = view.FindViewById<Button>(Resource.Id.btSavePasssword);

            _controlSigningHelper.SignControl(tvOldPassword, Strings.EnterOldPasswordTipLabel);
            _controlSigningHelper.SignControl(tvNewPassword, Strings.EnterNewPasswordTipLabel);
            _controlSigningHelper.SignControl(tvConfirmNewPassword, Strings.ConfirmNewPasswordTipLabel);
            _controlSigningHelper.SignControl(btSavePasssword, Strings.SaveChangesButtonLabel);
        }
    }
}