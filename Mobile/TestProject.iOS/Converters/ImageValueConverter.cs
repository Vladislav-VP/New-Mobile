using System;
using System.Globalization;

using MvvmCross;
using MvvmCross.Converters;
using MvvmCross.Plugin.PictureChooser.Platforms.Ios;
using UIKit;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.iOS.Converters
{
    public class ImageValueConverter : MvxValueConverter<byte[] , UIImage>
    {
        private static readonly MvxInMemoryImageValueConverter _converter;

        static ImageValueConverter()
        {
            _converter = new MvxInMemoryImageValueConverter();
        }

        protected override UIImage Convert(byte[] imageBytes, Type targetType, object parameter, CultureInfo culture)
        {
            if (imageBytes != null)
            {
                UIImage image = (UIImage)_converter.Convert(imageBytes, targetType, parameter, culture);

                return image;
            }

            return UIImage.FromBundle("ic_profile");
        }
    }
}