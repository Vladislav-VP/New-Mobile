using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TestProject.Core.ViewModels;
using TestProject.Resources;
using UIKit;

namespace TestProject.iOS.Views
{
    public partial class TodoItemListViewController : MvxViewController<TodoItemListViewModel>
    {
        public TodoItemListViewController() : base(nameof(TodoItemListViewController), null)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationController.SetNavigationBarHidden(false, true);

            

            var set = this.CreateBindingSet<TodoItemListViewController, TodoItemListViewModel>();


            set.Apply();
        }        


        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ViewModel.GoBackCommand.Execute(null);
        }
    }
}