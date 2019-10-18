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
    [MvxFragmentPresentation(typeof(MainViewModel),  Resource.Id.content_frame)]
    [Register("testProject.droid.fragments.TodoItemListFragment")]

    public class TodoItemListFragment : BaseFragment<TodoItemListViewModel>
    {
        protected override int FragmentId => Resource.Layout.TodoItemListFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            TextView tvTaskListTitle = view.FindViewById<TextView>(Resource.Id.tvTaskListTitle);

            tvTaskListTitle.Text = Strings.TaskListLabel;
            ParentActivity.SupportActionBar.Title = string.Empty;
            ((MainActivity)Activity).DrawerLayout.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);

            return view;
        }
    }
}