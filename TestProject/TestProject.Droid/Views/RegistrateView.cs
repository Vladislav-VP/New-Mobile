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

namespace TestProject.Droid.Views
{
    public class RegistrateView : BaseFragment<RegistrateViewModel>
    {
        protected override int FragmentId => Resource.Layout.LoginTemplate;
    }
}