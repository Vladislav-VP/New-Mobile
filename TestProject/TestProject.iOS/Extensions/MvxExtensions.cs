using MvvmCross.Platform.UI;
using UIKit;

namespace TestProject.iOS.Extensions
{
    public static class MvxExtensions
    {
        public static UIColor ToNativeColor(this MvxColor mvxColor)
        {
            return new UIColor(mvxColor.R, mvxColor.G, mvxColor.B, mvxColor.A);
        }
    }
}