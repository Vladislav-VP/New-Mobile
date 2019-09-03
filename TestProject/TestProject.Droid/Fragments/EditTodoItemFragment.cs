using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;

using TestProject.Core.ViewModels;
using TestProject.Droid.Activities;
using TestProject.Resources;

namespace TestProject.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, AddToBackStack = true)]
    [Register("testProject.droid.fragments.EditTodoItemFragment")]
    public class EditTodoItemFragment : BaseFragment<EditTodoItemViewModel>
    {
        protected override int FragmentId => Resource.Layout.EditTodoItemFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            InitializeAllControls(view);

            ((MainActivity)Activity).DrawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);

            return view;
        }

        protected override void InitializeAllControls(View view)
        {
            TextView tvTodoItemName = view.FindViewById<TextView>(Resource.Id.tvTodoItemName);
            TextView tvDescription = view.FindViewById<TextView>(Resource.Id.tvDescription);
            TextView tvDone = view.FindViewById<TextView>(Resource.Id.tvDone);
            TextInputEditText etTodoItemName = view.FindViewById<TextInputEditText>(Resource.Id.etTodoItemName);
            Button btSaveTodoItem = view.FindViewById<Button>(Resource.Id.btSaveTodoItem);
            Button btDeleteTodoItem = view.FindViewById<Button>(Resource.Id.btDeleteTodoItem);

            _controlSigningHelper.SignControl(tvTodoItemName, Strings.TodoItemNameTextViewLabel);
            _controlSigningHelper.SignControl(tvDescription, Strings.TodoItemDescriptionTextViewLabel);
            _controlSigningHelper.SignControl(tvDone, Strings.TodoItemIsDoneTextViewLabel);
            _controlSigningHelper.SignControl(btSaveTodoItem, Strings.SaveButtonLabel);
            _controlSigningHelper.SignControl(btDeleteTodoItem, Strings.DeleteTodoItemButtonLabel);

            etTodoItemName.Enabled = false;
        }
    }
}