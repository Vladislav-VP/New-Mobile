using Android.Widget;

using TestProject.Droid.Helpers.Interfaces;

namespace TestProject.Droid.Helpers
{
    public class ControlSigningHelper : IControlSigningHelper
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