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
        private TextView _showCurrentTime;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            TimePicker pickTime = FindViewById<TimePicker>(Resource.Id.timePicker1);
            _showCurrentTime = FindViewById<TextView>(Resource.Id.txtShowTime);
            SetCurrentTime();
            Button button = FindViewById<Button>(Resource.Id.btnSetTime);
            button.Click += delegate
            {
                _showCurrentTime.Text = string.Format("{0}:{1}",
                    pickTime.CurrentHour, pickTime.CurrentMinute);
            };
        }

        private void SetCurrentTime()
        {
            string time = string.Format("{0}", 
                DateTime.Now.ToString("HH:mm").PadLeft(2, '0'));
            _showCurrentTime.Text = time;
        }
    }
}