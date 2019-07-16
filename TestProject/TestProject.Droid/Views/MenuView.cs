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

namespace TestProject.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("testProject.droid.views.MenuView")]
    public class MenuView : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private NavigationView _navigationView;
        private IMenuItem _previousMenuItem;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.MenuView, null);

            _navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            _navigationView.SetNavigationItemSelectedListener(this);

            var todoItemsMenuItem = _navigationView.Menu.FindItem(Resource.Id.nav_todoItems);
            todoItemsMenuItem.SetCheckable(false);
            todoItemsMenuItem.SetChecked(false);

            _previousMenuItem = todoItemsMenuItem;

            var settingsMenuItem = _navigationView.Menu.FindItem(Resource.Id.nav_settings);
            settingsMenuItem.SetCheckable(false);

            var logoutMenuItem = _navigationView.Menu.FindItem(Resource.Id.nav_logout);
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
            ((MainView)Activity).DrawerLayout.CloseDrawers();
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
                    ViewModel.ShowLoginViewModelCommand.Execute(null);
                    break;
            }
        }
    }
}