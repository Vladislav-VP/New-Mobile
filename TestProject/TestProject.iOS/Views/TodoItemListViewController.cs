using System.Collections.Generic;

using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.iOS.Helpers.Interfaces;
using TestProject.iOS.Sources;
using TestProject.Resources;

namespace TestProject.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Tasks")]
    public partial class TodoItemListViewController : MvxViewController<TodoItemListViewModel>, IControlsSettingHelper
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeAllControls();

            CreateBindings();
        }

        public void InitializeAllControls()
        {
            Title = Strings.TaskListLabel;

            // TODO: Provide normal appearance for NavigationItem.RightBarButtonItem.
            var btAddTodoItem = new UIBarButtonItem();
            NavigationItem.RightBarButtonItem = btAddTodoItem;
            btAddTodoItem.Title = "+";
            btAddTodoItem.Style = UIBarButtonItemStyle.Plain;
            btAddTodoItem.TintColor = UIColor.Black;
            var addTodoItemFont = UIFont.SystemFontOfSize(Constants.MainFontSize, UIFontWeight.Semibold);
            var titleAttributes = new UITextAttributes()
            {
                TextColor = UIColor.White,
                Font = addTodoItemFont
            };
            btAddTodoItem.SetTitleTextAttributes(titleAttributes, UIControlState.Normal);
            NavigationItem.RightBarButtonItem.Clicked += AddTodoItemClicked;

            NavigationController.Toolbar.Hidden = false;
        }

        public void CreateBindings()
        {
            var source = new TableSource(tvTodoList, Constants.TodoListItemBindingText);
            var bindingMap = new Dictionary<object, string>();
            bindingMap.Add(source, Constants.TodoItemsBindingText);
            this.AddBindings(bindingMap);
            tvTodoList.Source = source;
            tvTodoList.ReloadData();

            var set = this.CreateBindingSet<TodoItemListViewController, TodoItemListViewModel>();

            set.Bind(source).For(v => v.ItemsSource).To(vm => vm.TodoItems);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.SelectTodoItemCommand);

            set.Apply();
        }

        private async void AddTodoItemClicked(object sender, System.EventArgs e)
        {
            if (ViewModel.AddTodoItemCommand != null)
            {
                await ViewModel.AddTodoItemCommand.ExecuteAsync(null);
            }
        }
    }
}