using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Platforms.Ios.Views;
using UIKit;
using TestProject.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;

namespace TestProject.iOS.Views
{
    [MvxRootPresentation]
    public class MainViewController : MvxViewController<MainViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<MainViewController, MainViewModel>();
            
            set.Apply();

            if (ViewModel.User == null)
            {
                ViewModel.GoToLoginCommand.Execute(null);
            }
        }
    }
}