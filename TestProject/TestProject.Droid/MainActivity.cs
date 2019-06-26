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
        private TextView _showCurrentDate;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            DatePicker pickDate = FindViewById<DatePicker>(Resource.Id.datePicker1);
            _showCurrentDate = FindViewById<TextView>(Resource.Id.txtShowDate);
            SetCurrentDate();
            Button button = FindViewById<Button>(Resource.Id.btnSetDate);
            button.Click += delegate
            {
                _showCurrentDate.Text = string.Format("{0}/{1}/{2}",
                    pickDate.Month, pickDate.DayOfMonth, pickDate.Year);
            };
        }

        private void SetCurrentDate()
        {
            string todaysDate = string.Format("{0}", 
                DateTime.Now.ToString("M/d/yyy").PadLeft(2, '0'));
            _showCurrentDate.Text = todaysDate;
        }
    }
}