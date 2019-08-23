using System;
using System.Threading.Tasks;

using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

using TestProject.Core.ViewModels;
using TestProject.Droid.Activities;

namespace TestProject.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.navigation_frame)]
    [Register("testProject.droid.views.MenuFragment")]
    public class MenuFragment : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private IMenuItem _previousMenuItem;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.MenuFragment, null);

            var navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            navigationView.SetNavigationItemSelectedListener(this);

            var todoItemsMenuItem = navigationView.Menu.FindItem(Resource.Id.nav_todoItems);
            todoItemsMenuItem.SetCheckable(false);
            todoItemsMenuItem.SetChecked(true);

            _previousMenuItem = todoItemsMenuItem;

            var settingsMenuItem = navigationView.Menu.FindItem(Resource.Id.nav_settings);
            settingsMenuItem.SetCheckable(false);

            var logoutMenuItem = navigationView.Menu.FindItem(Resource.Id.nav_logout);
            logoutMenuItem.SetCheckable(false);

            return view;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            if (_previousMenuItem != null)
                _previousMenuItem.SetChecked(false);

            item.SetCheckable(true);
            item.SetChecked(true);

            _previousMenuItem = item;

            Task.Run(() => Navigate(item.ItemId));

            return true;
        }

        private async Task Navigate(int itemId)
        {
            ((MainActivity)Activity).DrawerLayout.CloseDrawers();
            await Task.Delay(TimeSpan.FromMilliseconds(250));

            switch (itemId)
            {
                case Resource.Id.nav_settings:
                    ViewModel.ShowUserInfoViewModelCommand.Execute(null);
                    break;
                case Resource.Id.nav_todoItems:
                    ViewModel.ShowListTodoItemsViewModelCommand.Execute(null);
                    break;
                case Resource.Id.nav_logout:
                    ViewModel.LogoutCommand.Execute(null);
                    break;
            }
        }
    }
}