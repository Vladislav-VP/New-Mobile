using Android.App;

using TestProject.Droid.Helpers.Interfaces;

namespace TestProject.Droid.Helpers
{
    public class ActivityStorageHelper : IActivityStorageHelper
    {
        private Activity _activity;

        public void ReplaceActivity(Activity activity)
        {
            if (_activity != null)
            {
                _activity.Finish();
            }
            _activity = activity;
        }
    }
}