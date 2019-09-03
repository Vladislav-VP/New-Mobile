using Android.Views;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.ViewModels;

using TestProject.Droid.Helpers.Interfaces;

namespace TestProject.Droid.Fragments
{
    public abstract class BaseDialogFragment<TViewModel> : MvxDialogFragment<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        protected readonly IControlSigningHelper _controlSigningHelper;

        public BaseDialogFragment()
        {
            _controlSigningHelper = Mvx.IoCProvider.Resolve<IControlSigningHelper>();
        }

        protected abstract int FragmentId { get; }

        protected abstract void InitializeAllControls(View view);
    }
}