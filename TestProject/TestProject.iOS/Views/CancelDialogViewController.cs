using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.iOS.Views
{
    [MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen)]
    public partial class CancelDialogViewController : BaseViewController<CancelDialogViewModel>
    {
        public override void InitializeAllControls()
        {
            lbSaveChanges.Text = Strings.SaveChangesPrompt;
            btYes.SetTitle(Strings.YesButtonLabel, UIControlState.Normal);
            btNo.SetTitle(Strings.NoButtonLabel, UIControlState.Normal);
            btCancel.SetTitle(Strings.CancelButtonLabel, UIControlState.Normal);
        }

        public override void CreateBindings()
        {
            var set = this.CreateBindingSet<CancelDialogViewController, CancelDialogViewModel>();

            set.Bind(btYes).To(vm => vm.SaveCommand);
            set.Bind(btNo).To(vm => vm.DoNotSaveCommand);
            set.Bind(btCancel).To(vm => vm.CancelCommand);

            set.Apply();
        }
    }
}