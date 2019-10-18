using Android.OS;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace TestProject.Droid.Fragments
{
    public abstract class BaseDialogFragment<TViewModel> : MvxDialogFragment<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        protected abstract int FragmentId { get; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View ignore = base.OnCreateView(inflater, container, savedInstanceState);

            View view = this.BindingInflate(FragmentId, null);

            return view;
        }
    }
}