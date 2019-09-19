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
            NavigationItem.RightBarButtonItem = new UIBarButtonItem();
            NavigationItem.RightBarButtonItem.Image = UIImage.FromFile("ic_add_todoitem.png");
            NavigationItem.RightBarButtonItem.Style = UIBarButtonItemStyle.Plain;
            NavigationItem.RightBarButtonItem.TintColor = UIColor.Black;
            NavigationItem.RightBarButtonItem.Clicked += AddTodoItemClicked;
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