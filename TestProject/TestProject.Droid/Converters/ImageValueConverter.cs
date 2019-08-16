using Android.Graphics;
using Android.Util;
using DE.Hdodenhof.CircleImageViewLib;
using MvvmCross;
using MvvmCross.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
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
            
            var contextHelper = Mvx.IoCProvider.Resolve<IContextProvider>();
            var context = contextHelper.Context;

            var placeholder = BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.profile);

            return placeholder;
        }
    }
}
