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
using TestProject.Core.ViewModels;

namespace TestProject.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_layout)]
    [Register("testProject.droid.views.UserInfoView")]
    public class UserInfoView : BaseFragment<UserInfoViewModel>
    {
        protected override int FragmentId => throw new NotImplementedException();
    }
}