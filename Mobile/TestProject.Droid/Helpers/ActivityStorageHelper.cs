using Android.App;

using TestProject.Droid.Helpers.Interfaces;

namespace TestProject.Droid.Helpers
{
    public class ActivityReplaceHelper : IActivityReplaceHelper
    {
        private Activity _activity;

        public void ReplaceActivity(Activity activity)
        {
            _activity?.Finish();
            _activity = activity;
        }
    }
}