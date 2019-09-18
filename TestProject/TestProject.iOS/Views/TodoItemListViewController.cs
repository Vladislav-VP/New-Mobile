using System.Collections.Generic;

using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Color.Platforms.Ios;
using MvvmCross.ViewModels;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.Entities;
using TestProject.iOS.Helpers.Interfaces;
using TestProject.iOS.Sources;
using TestProject.Resources;
using MvvmCross;

namespace TestProject.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Tasks")]
    public partial class TodoItemListViewController : MvxViewController<TodoItemListViewModel>, IControlsSettingHelper
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }

        public override void ViewWillAppear(bool animated)
        {

        }

        public void CreateBindings()
        {
            throw new System.NotImplementedException();
        }

        public void InitializeAllControls()
        {
            throw new System.NotImplementedException();
        }

    }
}