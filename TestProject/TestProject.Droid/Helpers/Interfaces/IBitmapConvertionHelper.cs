using Android.Graphics;

namespace TestProject.Droid.Helpers.Interfaces
{
    public interface IBitmapConvertionHelper
    {
        Bitmap DecryptBitmap(string encodedBitmap);
    }
}