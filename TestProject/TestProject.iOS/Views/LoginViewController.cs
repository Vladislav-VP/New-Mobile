using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using TestProject.Core.ViewModels;
using TestProject.Resources;
using UIKit;

namespace TipCalc.iOS
{
    public partial class LoginViewController : MvxViewController<LoginViewModel>
    {
        public LoginViewController() : base(nameof(LoginViewController), null)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<LoginViewController, LoginViewModel>();


            //set.Bind(TipLabel).To(vm => vm.Tip);
            //set.Bind(SubTotalTextField).To(vm => vm.SubTotal);
            //set.Bind(GenerositySlider).To(vm => vm.Generosity);
            set.Apply();
        }
    }
}