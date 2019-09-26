using System;
using System.Globalization;

using MvvmCross;
using MvvmCross.Converters;
using MvvmCross.Plugin.PictureChooser.Platforms.Ios;
using UIKit;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.iOS.Converters
{
    public class ImageValueConverter : MvxValueConverter<string , UIImage>
    {
        private static readonly MvxInMemoryImageValueConverter _converter;

        private readonly IEncryptionHelper _encryptionHelper;

        static ImageValueConverter()
        {
            _converter = new MvxInMemoryImageValueConverter();
        }

        public ImageValueConverter(IEncryptionHelper encryptionHelper)
        {
            _encryptionHelper = encryptionHelper;
        }

        protected override UIImage Convert(string encryptedImage, Type targetType, object parameter, CultureInfo culture)
        {
            if (encryptedImage != null)
            {
                byte[] decryptedBytes = _encryptionHelper.DecryptBase64String(encryptedImage);

                UIImage image = (UIImage)_converter.Convert(decryptedBytes, targetType, parameter, culture);

                return image;
            }

            return UIImage.FromBundle("ic_profile");
        }
    }
}