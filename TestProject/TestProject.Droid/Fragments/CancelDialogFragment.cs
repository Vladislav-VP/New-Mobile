using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;

using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.Droid.Fragments
{
    [Register("testProject.droid.views.CancelDialogFragment")]
    public class CancelDialogFragment : MvxDialogFragment<CancelDialogViewModel>
    {
        protected int FragmentId => Resource.Layout.CancelDialogFragment;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View ignore = base.OnCreateView(inflater, container, savedInstanceState);

            View view = this.BindingInflate(FragmentId, null);
            Cancelable = false;

            InitializeAllControls(view);

            return view;
        }

        private void InitializeAllControls(View view)
        {
            TextView tvSaveChanges = view.FindViewById<TextView>(Resource.Id.tvSaveChanges);
            Button btYes = view.FindViewById<Button>(Resource.Id.btYes);
            Button btNo = view.FindViewById<Button>(Resource.Id.btNo);
            Button btCancel = view.FindViewById<Button>(Resource.Id.btCancel);

            InitializeControl(tvSaveChanges, ControlsLabels.SaveChangesPrompt);
            InitializeControl(btYes, ControlsLabels.YesButtonLabel);
            InitializeControl(btNo, ControlsLabels.NoButtonLabel);
            InitializeControl(btCancel, ControlsLabels.CancelButtonLabel);
        }

        // TODO: Create method for initializing controls.
        private void InitializeControl(TextView control, string label)
        {            
            if (control != null)
            {
                control.Text = label;
            }
        }
    }
}