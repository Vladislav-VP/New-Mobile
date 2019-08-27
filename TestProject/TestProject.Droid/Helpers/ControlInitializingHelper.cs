using Android.Widget;

using TestProject.Droid.Helpers.Interfaces;

namespace TestProject.Droid.Helpers
{
    public class ControlInitializingHelper : IControlInitializingHelper
    {
        public void SignControl(TextView control, string label)
        {
            if (control != null)
            {
                control.Text = label;
            }
        }
    }
}