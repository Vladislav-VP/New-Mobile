using System.IO;
using System.Threading.Tasks;

using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class PhotoEditHelper : IPhotoEditHelper
    {
        private readonly IPermissionsHelper _permissionsHelper;

        private readonly IPhotoCaptureHelper _photoCaptureHelper;

        private readonly IEncryptionHelper _encryptionHelper;

        public PhotoEditHelper(IPermissionsHelper permissionsHelper,
            IPhotoCaptureHelper photoCaptureHelper, IEncryptionHelper encryptionHelper)
        {
            _permissionsHelper = permissionsHelper;
            _photoCaptureHelper = photoCaptureHelper;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<byte[]> PickPhoto()
        {
            MediaFile file = await _photoCaptureHelper.PickPhoto();
            byte[] imageBytes = GetImage(file);

            return imageBytes;
        }

        public async Task<byte[]> TakePhoto()
        {
            byte[] imageBytes = null;

            bool isCameraPermitted = await _permissionsHelper.IsPermissionGranted(Permission.Camera);
            bool isStoragePermitted = await _permissionsHelper.IsPermissionGranted(Permission.Storage);
            if (isCameraPermitted && isStoragePermitted)
            {
                MediaFile file = await _photoCaptureHelper.TakePhoto();
                imageBytes = GetImage(file);
            }

            return imageBytes;
        }

        public Task<byte[]> DeletePhoto()
        {
            return Task.FromResult<byte[]>(null);
        }

        private byte[] GetImage(MediaFile file)
        {
            if (file == null)
            {
                return null;
            }
            byte[] imageBytes = null;
            using (Stream imageStream = file.GetStream())
            {
                imageBytes = _encryptionHelper.GetBytes(imageStream);
            }

            return imageBytes;
        }
    }
}
