using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Color.Platforms.Ios;
using MvvmCross.UI;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using TestProject.Core.ViewModels;
using TestProject.Entities;
using TestProject.iOS.Helpers.Interfaces;
using TestProject.iOS.Sources;
using TestProject.Resources;
using UIKit;

namespace TestProject.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Menu", TabIconName = "ic_menu")]
    public partial class MenuViewController : MvxViewController<MenuViewModel>, IControlsSettingHelper
    {
        public MenuViewController() : base(nameof(MenuViewController), null)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeAllControls();

            CreateBindings();
        }

        public void CreateBindings()
        {
        }

        public void InitializeAllControls()
        {

            var set = this.CreateBindingSet<MenuViewController, MenuViewModel>();

            set.Apply();
        }
    }
}