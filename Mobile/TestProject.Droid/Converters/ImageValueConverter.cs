using System;
using System.Globalization;

using Android.Graphics;
using MvvmCross.Converters;
using Plugin.CurrentActivity;

using TestProject.Droid.Helpers.Interfaces;

namespace TestProject.Droid.Converters
{
    public class ImageValueConverter : MvxValueConverter<string, Bitmap>
    {
        private readonly IBitmapConvertionHelper _bitmapConvertionHelper;

        public ImageValueConverter(IBitmapConvertionHelper bitmapConvertionHelper)
        {
            _bitmapConvertionHelper = bitmapConvertionHelper;
        }

        protected override Bitmap Convert(string encodedImage, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(encodedImage))
            {
                Bitmap bitmap = _bitmapConvertionHelper.DecryptBitmap(encodedImage);

                return bitmap;
            }

            Android.Content.Res.Resources resources = CrossCurrentActivity
                .Current
                .Activity
                .Resources;

            Bitmap placeholder = BitmapFactory.DecodeResource(resources, Resource.Mipmap.profile);

            return placeholder;
        }
    }
}
