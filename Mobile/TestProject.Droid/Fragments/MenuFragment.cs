using System;
using System.Threading.Tasks;

using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

using TestProject.Core.ViewModels;
using TestProject.Droid.Activities;
using TestProject.Resources;

namespace TestProject.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.navigation_frame, AddToBackStack = false)]
    [Register("testProject.droid.fragments.MenuFragment")]
    public class MenuFragment : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private IMenuItem _previousMenuItem;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View ignore = base.OnCreateView(inflater, container, savedInstanceState);

            View view = this.BindingInflate(Resource.Layout.MenuFragment, null);

            NavigationView navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            navigationView.SetNavigationItemSelectedListener(this);
            
            IMenuItem todoItemsMenuItem = navigationView.Menu.FindItem(Resource.Id.nav_todoItems);
            todoItemsMenuItem.SetCheckable(false);
            todoItemsMenuItem.SetChecked(true);

            _previousMenuItem = todoItemsMenuItem;

            IMenuItem settingsMenuItem = navigationView.Menu.FindItem(Resource.Id.nav_settings);
            settingsMenuItem.SetCheckable(false);

            IMenuItem logoutMenuItem = navigationView.Menu.FindItem(Resource.Id.nav_logout);
            logoutMenuItem.SetCheckable(false);

            SignMenuItem(settingsMenuItem, Strings.SettingsLabel);
            SignMenuItem(todoItemsMenuItem, Strings.TaskListLabel);
            SignMenuItem(logoutMenuItem, Strings.LogoutLabel);

            return view;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            _previousMenuItem?.SetChecked(false);

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
                    ViewModel.ShowSettingsCommand.Execute(null);
                    break;
                case Resource.Id.nav_todoItems:
                    ViewModel.ShowTodoItemListCommand.Execute(null);
                    break;
                case Resource.Id.nav_logout:
                    ViewModel.LogoutCommand.Execute(null);
                    break;
            }
        }

        private void SignMenuItem(IMenuItem menuItem, string title)
        {
            menuItem.SetActionView(new TextView(Context));
            ((TextView)menuItem.ActionView).Text = title;

            int textSize = 20;
            ((TextView)menuItem.ActionView).TextSize = textSize;

            int left = 0;
            int top = 20;
            int right = 0;
            int bottom = 0;

            menuItem.ActionView.SetPadding(left, top, right, bottom);
        }
    }
}