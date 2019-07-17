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
using TestProject.Core.ViewModels;
using TestProject.Core.Resources;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace TestProject.Droid.Views
{
    [Register("testProject.droid.views.RegistrateView")]
    public class RegistrateUserView : BaseFragment<RegistrateUserViewModel>
    {
        protected override int FragmentId => Resource.Layout.RegistrateUserView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }
    }
}