using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;

using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.Droid.Fragments
{
    [MvxDialogFragmentPresentation(Cancelable = false)]
    [Register("testProject.droid.fragments.CancelDialogFragment")]
    public class CancelDialogFragment : BaseDialogFragment<CancelDialogViewModel>
    {
        protected override int FragmentId => Resource.Layout.CancelDialogFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            TextView tvSaveChanges = view.FindViewById<TextView>(Resource.Id.tvSaveChanges);
            Button btYes = view.FindViewById<Button>(Resource.Id.btYes);
            Button btNo = view.FindViewById<Button>(Resource.Id.btNo);
            Button btCancel = view.FindViewById<Button>(Resource.Id.btCancel);

            tvSaveChanges.Text = Strings.SaveChangesPrompt;
            btYes.Text = Strings.YesButtonLabel;
            btNo.Text = Strings.NoButtonLabel;
            btCancel.Text = Strings.CancelButtonLabel;

            return view;
        }
    }
}