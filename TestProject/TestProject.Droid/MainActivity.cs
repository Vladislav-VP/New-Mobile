using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace TestProject.Droid
{
    [Activity(Label = "options Menu", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            GridView gridView = FindViewById<GridView>(Resource.Id.gridView1);
            gridView.Adapter = new ImageAdapter(this);
            gridView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs e)
              {
                  Toast.MakeText(this, (e.Position + 1).ToString(), ToastLength.Short).Show();
              };
        }
    }
}