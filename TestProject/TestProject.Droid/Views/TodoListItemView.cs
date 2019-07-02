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

using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross;
using MvvmCross.Platforms.Android.Views;
using TestProject.Core.ViewModels;
using MvvmCross.Droid.Support.V4;

namespace TestProject.Droid.Views
{
    [MvxFragmentPresentation]
    public class TodoListItemView : MvxFragment<TodoListItemViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}