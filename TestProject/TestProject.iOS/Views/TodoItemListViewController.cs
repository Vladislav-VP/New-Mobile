using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Color.Platforms.Ios;
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
    [MvxRootPresentation]
    public partial class TodoItemListViewController : MvxViewController<TodoItemListViewModel>, IControlsSettingHelper
    {
        public TodoItemListViewController() : base(nameof(TodoItemListViewController), null)
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
            var source = new TableSource(tbviewTodoItems, Constants.TodoListItemBindingText);
            var bindingMap = new Dictionary<object, string>
            {
                { source, Constants.TodoItemsBindingText }
            };
            this.AddBindings(bindingMap);
            tbviewTodoItems.Source = source;
            tbviewTodoItems.ReloadData();

            // TODO : Remove 2 lines below (mocked todoitem)
            ViewModel.TodoItems = new MvxObservableCollection<TodoItem>();
            ViewModel.TodoItems.Add(new TodoItem { Name = "1", Description = "1" });

            var set = this.CreateBindingSet<TodoItemListViewController, TodoItemListViewModel>();

            set.Apply();
        }

        public void InitializeAllControls()
        {
            Title = Strings.TaskListLabel;
            UINavigationBar.Appearance.BarTintColor = AppColors.MainInterfaceBlue.ToNativeColor();
            //NavigationController.SetNavigationBarHidden(false, true);
            NavigationItem.SetHidesBackButton(true, false);
        }
    }
}