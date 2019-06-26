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
            Button button = FindViewById<Button>(Resource.Id.alertButton);
            button.Click += delegate
            {
                AlertDialog.Builder alertDiag = new AlertDialog.Builder(this);
                alertDiag.SetTitle("Confirm delete");
                alertDiag.SetMessage("Once deleted the move cannot be undone");
                alertDiag.SetPositiveButton("Delete", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "Deleted", ToastLength.Short).Show();
                });
                alertDiag.SetNegativeButton("Cancel", (senderAlert, args) =>
                {
                    alertDiag.Dispose();
                });
                Dialog dialog = alertDiag.Create();
                dialog.Show();
            };
        }
    }
}