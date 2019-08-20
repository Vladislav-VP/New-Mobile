using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;

using TestProject.Core.ViewModels;
using TestProject.Droid.Activities;
using TestProject.Resources;

namespace TestProject.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("testProject.droid.views.TodoListItemFragment")]

    public class TodoListItemFragment : BaseFragment<TodoListItemViewModel>
    {
        protected override int FragmentId => Resource.Layout.TodoListItemFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ((MainActivity)Activity).DrawerLayout.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);

            var taskList = view.FindViewById<MvxRecyclerView>(Resource.Id.todoItemsRecyclerView);

            ParentActivity.SupportActionBar.Title = Strings.TaskList;

            return view;
        }
    }
}