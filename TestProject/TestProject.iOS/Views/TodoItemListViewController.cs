using System.Collections.Generic;

using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.iOS.Helpers.Interfaces;
using TestProject.iOS.Views.Cells;
using TestProject.Resources;

namespace TestProject.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Tasks", TabIconName = "ic_tasks")]
    public partial class TodoItemListViewController : MvxViewController<TodoItemListViewModel>, IControlsSettingHelper
    {
        private MvxUIRefreshControl _refreshControl;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeAllControls();

            CreateBindings();
        }

        public void InitializeAllControls()
        {
            Title = Strings.TaskListLabel;

            var btAddTodoItem = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            btAddTodoItem.TintColor = UIColor.White;
            btAddTodoItem.Clicked += AddTodoItemClicked;
            NavigationItem.RightBarButtonItem = btAddTodoItem;

            NavigationController.Toolbar.Hidden = false;

            _refreshControl = new MvxUIRefreshControl();
            tvTodoList.AddSubview(_refreshControl);
        }

        public void CreateBindings()
        {
            var source = new MvxSimpleTableViewSource(tvTodoList, typeof(TodoItemTableViewCell));
            var bindingMap = new Dictionary<object, string>();
            bindingMap.Add(source, Constants.TodoItemsBindingText);
            this.AddBindings(bindingMap);
            tvTodoList.Source = source;
            tvTodoList.ReloadData();

            var set = this.CreateBindingSet<TodoItemListViewController, TodoItemListViewModel>();

            set.Bind(source).For(v => v.ItemsSource).To(vm => vm.TodoItems);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.SelectTodoItemCommand);
            set.Bind(_refreshControl).For(r => r.IsRefreshing).To(vm => vm.LoadTodoItemsTask.IsNotCompleted).WithFallback(false);
            set.Bind(_refreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshTodoItemsCommand);

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