using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding.BindingContext;

using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.Droid.Fragments
{
    [Register("testProject.droid.views.CancelDialogFragment")]
    public class CancelDialogFragment : BaseDialogFragment<CancelDialogViewModel>
    {
        protected override int FragmentId => Resource.Layout.CancelDialogFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View ignore = base.OnCreateView(inflater, container, savedInstanceState);

            View view = this.BindingInflate(FragmentId, null);
            Cancelable = false;

            InitializeAllControls(view);

            return view;
        }

        protected override void InitializeAllControls(View view)
        {
            TextView tvSaveChanges = view.FindViewById<TextView>(Resource.Id.tvSaveChanges);
            Button btYes = view.FindViewById<Button>(Resource.Id.btYes);
            Button btNo = view.FindViewById<Button>(Resource.Id.btNo);
            Button btCancel = view.FindViewById<Button>(Resource.Id.btCancel);

            _controlSigningHelper.SignControl(tvSaveChanges, ControlsLabels.SaveChangesPrompt);
            _controlSigningHelper.SignControl(btYes, ControlsLabels.YesButtonLabel);
            _controlSigningHelper.SignControl(btNo, ControlsLabels.NoButtonLabel);
            _controlSigningHelper.SignControl(btCancel, ControlsLabels.CancelButtonLabel);
        }
    }
}