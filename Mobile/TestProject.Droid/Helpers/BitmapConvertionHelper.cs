using Android.Graphics;
using Android.Util;

using TestProject.Droid.Helpers.Interfaces;

namespace TestProject.Droid.Helpers
{
    public class BitmapConvertionHelper : IBitmapConvertionHelper
    {
        public Bitmap DecryptBitmap(string encodedBitmap)
        {
            byte[] decodedString = Base64.Decode(encodedBitmap, Base64Flags.Default);

            Bitmap decodedBytes = BitmapFactory.DecodeByteArray(decodedString, 0, decodedString.Length);

            return decodedBytes;
        }
    }
}