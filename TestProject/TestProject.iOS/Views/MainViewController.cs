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
    //[MvxRootPresentation]
    public class MainViewController : MvxTabBarViewController<MainViewModel>
    {
        private bool _firstTimePresented = true;

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<MainViewController, MainViewModel>();
            
            set.Apply();

            //if (await ViewModel.User == null)
            //{
            //    ViewModel.GoToLoginCommand.Execute(null);
            //    return;
            //}

            //ViewModel.ShowTodoItemListCommand.Execute(null);
            //if(await ViewModel.User != null)
            //{
                
            //}
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (_firstTimePresented)
            {
                _firstTimePresented = false;
                ViewModel.ShowTodoItemListCommand.Execute(null);
                ViewModel.ShowMenuCommand.Execute(null);
            }
        }

    }
}