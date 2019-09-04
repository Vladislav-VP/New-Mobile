using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;

using TestProject.Core.ViewModels;
using TestProject.Droid.Activities;
using TestProject.Resources;

namespace TestProject.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, AddToBackStack = false)]
    [Register("testProject.droid.fragments.TodoItemListFragment")]

    public class TodoItemListFragment : BaseFragment<TodoItemListViewModel>
    {
        protected override int FragmentId => Resource.Layout.TodoItemListFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            if (savedInstanceState == null && ViewModel.ShowMenuViewModelCommand != null)
            {
                ViewModel.ShowMenuViewModelCommand.Execute(null);
            }

            InitializeAllControls(view);

            return view;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();   
        }

        protected override void InitializeAllControls(View view)
        {
            TextView tvTaskListTitle = view.FindViewById<TextView>(Resource.Id.tvTaskListTitle);

            _controlSigningHelper.SignControl(tvTaskListTitle, Strings.TaskListLabel);
            ParentActivity.SupportActionBar.Title = string.Empty;
            ((MainActivity)Activity).DrawerLayout.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);
        }
    }
}