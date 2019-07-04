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

namespace TestProject.Droid.Views
{
    public class RegistrateView : BaseFragment<RegistrateViewModel>
    {
        protected override int FragmentId => Resource.Layout.LoginTemplate;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var registrateLayout = view.FindViewById<LinearLayout>(Resource.Id.registrateLayout);
            if (registrateLayout != null)
            {
                registrateLayout.Visibility = ViewStates.Invisible;
            }
            var loginButton = view.FindViewById<Button>(Resource.Id.btLogin);
            if (loginButton != null)
            {
                loginButton.Text = Strings.registrateUser;
            }

            return view;
        }
    }
}