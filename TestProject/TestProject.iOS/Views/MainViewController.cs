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
    public class MainViewController : MvxTabBarViewController<MainViewModel>
    {
        private bool IsStarted { get; set; } = false;
        public MainViewController()
        {

        }

        public void HideTabBar(bool needHidden = false)
        {
            if (TabBar != null)
            {
                TabBar.Hidden = needHidden;
                TabBar.Translucent = needHidden;
            }
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (!IsStarted)
            {
                await ViewModel.StartTabViewModels();
                SelectedIndex = 1;
                foreach (UITabBarItem tab in TabBar.Items)
                {
                    tab.ImageInsets = new UIEdgeInsets(5, -5, -5, -5);
                }
            }
            IsStarted = true;
        }

    }
}