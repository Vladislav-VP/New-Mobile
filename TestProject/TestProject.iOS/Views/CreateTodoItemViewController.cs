using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.iOS.Helpers.Interfaces;
using TestProject.Resources;

namespace TestProject.iOS.Views
{
    [MvxChildPresentation]
    public partial class CreateTodoItemViewController : MvxViewController<CreateTodoItemViewModel>, IControlsSettingHelper
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeAllControls();

            CreateBindings();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ParentViewController.TabBarItem.Enabled = false;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ParentViewController.TabBarItem.Enabled = true;
        }

        public void InitializeAllControls()
        {
            Title = Strings.NewTask;

            NavigationItem.LeftBarButtonItem = new UIBarButtonItem();
            NavigationItem.LeftBarButtonItem.Title = Strings.BackLabel;
            NavigationItem.LeftBarButtonItem.TintColor = UIColor.White;
            NavigationItem.LeftBarButtonItem.Clicked += CancelClicked;

            lbTaskName.Text = Strings.TodoItemNameTextViewLabel;
            lbDescription.Text = Strings.TodoItemDescriptionTextViewLabel;
            lbDone.Text = Strings.TodoItemIsDoneTextViewLabel;

            btSave.SetTitle(Strings.SaveButtonLabel, UIControlState.Normal);
        }

        public void CreateBindings()
        {
            var set = this.CreateBindingSet<CreateTodoItemViewController, CreateTodoItemViewModel>();

            set.Bind(tfTaskName).To(vm => vm.Name);
            set.Bind(tvDescription).To(vm => vm.Description);
            set.Bind(swDone).To(vm => vm.IsDone);
            set.Bind(btSave).To(vm => vm.CreateTodoItemCommand);

            set.Apply();
        }

        private void CancelClicked(object sender, System.EventArgs e)
        {
            ViewModel.GoBackCommand?.Execute(null);
        }
    }
}