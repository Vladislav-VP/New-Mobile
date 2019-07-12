using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Views;
using TestProject.Core.ViewModels;


namespace TestProject.Droid.Views
{
    [MvvmCross.Platforms.Android.Presenters.Attributes.MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_layout, true)]
    [Register("testProject.droid.views.EditTodoItemView")]
    public class EditTodoItemView : BaseFragment<EditTodoItemViewModel>
    {
        protected override int FragmentId => Resource.Layout.TodoItemTemplate;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            //View.

            return view;
        }
    }
}