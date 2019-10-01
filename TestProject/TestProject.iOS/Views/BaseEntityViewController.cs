using System;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.iOS.Views
{
    public abstract class BaseEntityViewController : BaseViewController
    {
        public new BaseEntityViewModel ViewModel
        {
            get => (BaseEntityViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        public override void InitializeAllControls()
        {
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem();
            NavigationItem.LeftBarButtonItem.Title = Strings.BackLabel;
            NavigationItem.LeftBarButtonItem.TintColor = UIColor.White;
            NavigationItem.LeftBarButtonItem.Clicked += CancelClicked;
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            ViewModel.GoBackCommand?.Execute(null);
        }
    }
}