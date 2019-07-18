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
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("testProject.droid.views.CreateTodoItemFragment")]
    public class CreateTodoItemFragment : BaseFragment<CreateTodoItemViewModel>
    {
        private Button _saveButton;

        protected override int FragmentId => Resource.Layout.CreateTodoItemFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }
    }
}