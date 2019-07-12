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
    [Register("testProject.droid.views.LoginView")]
    public class LoginView : BaseFragment<LoginViewModel>
    {
        protected override int FragmentId => Resource.Layout.LoginView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            //var btnRegistrate = view.FindViewById<Button>(Resource.Id.btnRegistrate);
            //if (btnRegistrate != null)
            //{
            //    btnRegistrate.Enabled = true;
            //}

            return view;
        }
    }
}