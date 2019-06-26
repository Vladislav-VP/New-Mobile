using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace TestProject.Droid
{
    //[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    [Activity(Label = "options Menu", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            //Button showPopupMenu = FindViewById<Button>(Resource.Id.popupButton);
            //showPopupMenu.Click += (s, arg) =>
            //{
            //    PopupMenu menu = new PopupMenu(this, showPopupMenu);
            //    menu.Inflate(Resource.Menu.popMenu);
            //    menu.Show();
            //};
        }

        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    MenuInflater.Inflate(Resource.Layout.myMenu, menu);
        //    return base.OnPrepareOptionsMenu(menu);
        //}

        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    if (item.ItemId == Resource.Id.file_settings)
        //    {
        //        return true;
        //    }
        //    return base.OnOptionsItemSelected(item);
        //}
    }
}