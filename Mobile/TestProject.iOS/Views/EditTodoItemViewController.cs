using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.iOS.Views
{
    [MvxChildPresentation]
    public partial class EditTodoItemViewController : BaseEntityViewController
    {
        public new EditTodoItemViewModel ViewModel
        {
            get => (EditTodoItemViewModel)base.ViewModel;
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

            Title = ViewModel.Name;

            lbTaskName.Text = Strings.TodoItemNameTextViewLabel;
            lbDescription.Text = Strings.TodoItemDescriptionTextViewLabel;
            lbDone.Text = Strings.TodoItemIsDoneTextViewLabel;

            btSave.SetTitle(Strings.SaveButtonLabel, UIControlState.Normal);
            btDelete.SetTitle(Strings.DeleteTodoItemButtonLabel, UIControlState.Normal);
        }

        public override void CreateBindings()
        {
            var set = this.CreateBindingSet<EditTodoItemViewController, EditTodoItemViewModel>();

            set.Bind(tfTaskName).To(vm => vm.Name);
            set.Bind(tvDescription).To(vm => vm.Description);
            set.Bind(swDone).To(vm => vm.IsDone);
            set.Bind(btSave).To(vm => vm.UpdateTodoItemCommand);
            set.Bind(btDelete).To(vm => vm.DeleteTodoItemCommand);

            set.Apply();
        }
    }
}