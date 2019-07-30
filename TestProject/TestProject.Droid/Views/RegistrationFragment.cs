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
using TestProject.Configurations;
using TestProject.Resources;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace TestProject.Droid.Views
{
    [Register("testProject.droid.views.RegistrationFragment")]
    public class RegistrationFragment : BaseFragment<RegistrationViewModel>
    {
        private const int MinPasswordLength = 6;

        protected override int FragmentId => Resource.Layout.RegistrationFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var tvPasswordTip = view.FindViewById<TextView>(Resource.Id.tvPasswordTip);
            tvPasswordTip.Text = "(can contain letters, digits and lower underlines, " +
                $"not shorter than {MinPasswordLength} characters)";

            return view;
        }
    }
}