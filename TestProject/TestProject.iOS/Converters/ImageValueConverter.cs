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

        static ImageValueConverter()
        {
            _converter = new MvxInMemoryImageValueConverter();
        }

        protected override UIImage Convert(string encryptedImage, Type targetType, object parameter, CultureInfo culture)
        {
            if (encryptedImage != null)
            {
                IEncryptionHelper encryptionHelper = Mvx.IoCProvider.Resolve<IEncryptionHelper>();

                byte[] decryptedBytes = encryptionHelper.DecryptBase64String(encryptedImage);

                UIImage image = (UIImage)_converter.Convert(decryptedBytes, targetType, parameter, culture);

                return image;
            }

            return UIImage.FromBundle("ic_profile");
        }
    }
}