using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;

using TestProject.Core.ViewModels;

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

            return view;
        }
    }
}