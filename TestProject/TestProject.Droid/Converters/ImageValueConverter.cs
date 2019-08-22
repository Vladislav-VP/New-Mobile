using System;
using System.Globalization;

using Android.Content;
using Android.Graphics;
using Android.Util;
using MvvmCross;
using MvvmCross.Converters;

using TestProject.Droid.Providers.Interfaces;

namespace TestProject.Droid.Converters
{
    public class ImageValueConverter : MvxValueConverter<string, Bitmap>
    {
        protected override Bitmap Convert(string encodedImage, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(encodedImage))
            {
                byte[] decodedString = Base64.Decode(encodedImage, Base64Flags.Default);

                Bitmap decodedBytes = BitmapFactory.DecodeByteArray(decodedString, 0, decodedString.Length);

                return decodedBytes;
            }

            IContextProvider contextProvider = Mvx.IoCProvider.Resolve<IContextProvider>();
            Context context = contextProvider.Context;

            Bitmap placeholder = BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.profile);

            return placeholder;
        }
    }
}
