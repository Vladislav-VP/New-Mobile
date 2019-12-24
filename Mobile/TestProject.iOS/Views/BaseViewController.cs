using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using UIKit;

namespace TestProject.iOS.Views
{
    public abstract class BaseViewController : MvxViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeAllControls();

            CreateBindings();
        }

        public abstract void InitializeAllControls();

        public abstract void CreateBindings();

        protected bool ResignFirstResponder(UIResponder responder)
        {
            responder.ResignFirstResponder();
            return true;
        }
    }

    public abstract class BaseViewController<TViewModel> : BaseViewController
    {
        public new TViewModel ViewModel
        {
            get => (TViewModel)base.ViewModel;
            set => base.ViewModel = (IMvxViewModel)value;
        }
    }
}