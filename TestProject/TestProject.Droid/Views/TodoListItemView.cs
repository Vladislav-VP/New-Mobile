using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;

using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross;
using MvvmCross.Platforms.Android.Views;
using TestProject.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;
using TestProject.Core.Resources;
using Android.Support.V4.View;

namespace TestProject.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("testProject.droid.views.TodoListItemFragment")]

    public class TodoListItemView : BaseFragment<TodoListItemViewModel>
    {
        protected override int FragmentId => Resource.Layout.TodoListItemView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ParentActivity.SupportActionBar.Title = Strings.TaskList;

            return view;
        }
    }
}