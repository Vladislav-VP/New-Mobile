using System;
using System.Globalization;

using Android.Graphics;
using MvvmCross.Converters;
using Plugin.CurrentActivity;

using TestProject.Droid.Helpers.Interfaces;

namespace TestProject.Droid.Converters
{
    public class ImageValueConverter : MvxValueConverter<byte[], Bitmap>
    {

        protected override Bitmap Convert(byte[] imageBytes, Type targetType, object parameter, CultureInfo culture)
        {
            if (imageBytes != null)
            {
                Bitmap bitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);

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
