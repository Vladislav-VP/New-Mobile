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

            _controlSigningHelper.SignControl(tvOldPassword, ControlsLabels.EnterOldPasswordTipLabel);
            _controlSigningHelper.SignControl(tvNewPassword, ControlsLabels.EnterNewPasswordTipLabel);
            _controlSigningHelper.SignControl(tvConfirmNewPassword, ControlsLabels.ConfirmNewPasswordTipLabel);
            _controlSigningHelper.SignControl(btSavePasssword, ControlsLabels.SaveChangesButtonLabel);
        }
    }
}