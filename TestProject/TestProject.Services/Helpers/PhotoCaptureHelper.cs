using System.Threading.Tasks;

using Plugin.Media;
using Plugin.Media.Abstractions;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class PhotoCaptureHelper : IPhotoCaptureHelper
    {
        public async Task<MediaFile> PickPhoto(int compressionQuality = 90, int maxPixelDimension = 200)
        {
            var options = new PickMediaOptions
            {
                CompressionQuality = compressionQuality,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = maxPixelDimension
            };

            return await CrossMedia.Current.PickPhotoAsync(options);
        }

        public async Task<MediaFile> TakePhoto(int compressionQuality = 90, int maxPixelDimension = 200)
        {
            bool initialized = await CrossMedia.Current.Initialize();
            var options = new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                CompressionQuality = compressionQuality,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = maxPixelDimension,
                DefaultCamera = CameraDevice.Rear
            };
            MediaFile file= await CrossMedia.Current.TakePhotoAsync(options);
            return await CrossMedia.Current.TakePhotoAsync(options);
        }
    }
}
