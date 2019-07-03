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
        protected override int FragmentId => Resource.Layout.CreateTodoItemView;
    }
}