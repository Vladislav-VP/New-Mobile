using System;
using System.Globalization;

using Android.Content;
using Android.Graphics;
using MvvmCross;
using MvvmCross.Converters;

using TestProject.Droid.Helpers.Interfaces;
using TestProject.Droid.Providers.Interfaces;

namespace TestProject.Droid.Converters
{
    public class ImageValueConverter : MvxValueConverter<string, Bitmap>
    {
        protected override Bitmap Convert(string encodedImage, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(encodedImage))
            {
                IBitmapConvertionHelper bitmapConvertionHelper = Mvx.IoCProvider.Resolve<IBitmapConvertionHelper>();

                Bitmap bitmap = bitmapConvertionHelper.DecryptBitmap(encodedImage);

                return bitmap;
            }

            IContextProvider contextProvider = Mvx.IoCProvider.Resolve<IContextProvider>();
            Context context = contextProvider.Context;

            Bitmap placeholder = BitmapFactory.DecodeResource(context.Resources, Resource.Mipmap.profile);

            return placeholder;
        }
    }
}
