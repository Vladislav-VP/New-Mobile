using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Views;
using TestProject.Core.ViewModels;
using TestProject.Core.Resources;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Android.Support.V7.Widget;

namespace TestProject.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_layout, true)]
    [Register("testProject.droid.views.CreateTodoItemView")]
    public class CreateTodoItemView : BaseFragment<CreateTodoItemViewModel>
    {
        protected override int FragmentId => Resource.Layout.TodoItemTemplate;

        private Button _saveButton;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var btDelete = view.FindViewById<Button>(Resource.Id.btDelete);
            if (btDelete != null)
            {
                btDelete.Enabled = false;
            }
            var headerText = view.FindViewById<TextView>(Resource.Id.taskNameTextView);
            if (headerText != null)
            {
                headerText.Text = Strings.newTask;
            }
            return view;
        }
    }
}