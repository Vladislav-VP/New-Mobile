using System.Threading.Tasks;

using Plugin.Media.Abstractions;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IPhotoCaptureHelper
    {
        Task<MediaFile> PickPhoto(int compressionQuality = 90, int maxPixelDimension = 200);

        Task<MediaFile> TakePhoto(int compressionQuality = 90, int maxPixelDimension = 200);
    }
}
