using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

using TestProject.Core.ViewModels;

namespace TestProject.iOS.Views
{
    [MvxRootPresentation]
    public class MainViewController : MvxTabBarViewController<MainViewModel>
    {
        private bool IsStarted { get; set; } = false;

        public void HideTabBar(bool needHidden = false)
        {
            if (TabBar != null)
            {
                TabBar.Hidden = needHidden;
                TabBar.Translucent = needHidden;
            }
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