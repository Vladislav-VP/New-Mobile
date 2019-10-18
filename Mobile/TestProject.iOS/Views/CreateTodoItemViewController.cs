using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.iOS.Views
{
    [MvxChildPresentation]
    public partial class CreateTodoItemViewController : BaseEntityViewController
    {
        public new CreateTodoItemViewModel ViewModel
        {
            get => (CreateTodoItemViewModel)base.ViewModel;
            set => base.ViewModel = value;
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

        public override void InitializeAllControls()
        {
            base.InitializeAllControls();

            Title = Strings.NewTask;

            lbTaskName.Text = Strings.TodoItemNameTextViewLabel;
            lbDescription.Text = Strings.TodoItemDescriptionTextViewLabel;
            lbDone.Text = Strings.TodoItemIsDoneTextViewLabel;

            btSave.SetTitle(Strings.SaveButtonLabel, UIControlState.Normal);
        }

        public override void CreateBindings()
        {
            var set = this.CreateBindingSet<CreateTodoItemViewController, CreateTodoItemViewModel>();

            set.Bind(tfTaskName).To(vm => vm.Name);
            set.Bind(tvDescription).To(vm => vm.Description);
            set.Bind(swDone).To(vm => vm.IsDone);
            set.Bind(btSave).To(vm => vm.CreateTodoItemCommand);

            set.Apply();
        }
    }
}