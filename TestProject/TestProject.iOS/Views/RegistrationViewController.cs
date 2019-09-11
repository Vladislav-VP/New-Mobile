using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using TestProject.Core.ViewModels;
using TestProject.Resources;
using UIKit;

namespace TipCalc.iOS
{
    public partial class RegistrationViewController : MvxViewController<RegistrationViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<RegistrationViewController, RegistrationViewModel>();

            set.Apply();
        }
    }
}