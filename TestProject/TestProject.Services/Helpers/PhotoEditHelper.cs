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

        public async Task<string> PickPhoto()
        {
            MediaFile file = await _photoCaptureHelper.PickPhoto();
            string encryptedImageString = EncryptImage(file);

            return encryptedImageString;
        }

        public async Task<string> TakePhoto()
        {
            string encryptedImageString = null;

            bool isCameraPermitted = await _permissionsHelper.IsPermissionGranted(Permission.Camera);
            bool isStoragePermitted = await _permissionsHelper.IsPermissionGranted(Permission.Storage);
            if (isCameraPermitted && isStoragePermitted)
            {
                MediaFile file = await _photoCaptureHelper.TakePhoto();
                encryptedImageString = EncryptImage(file);
            }

            return encryptedImageString;
        }

        private string EncryptImage(MediaFile file)
        {
            string encryptedImageString = null;
            using (Stream imageStream = file.GetStream())
            {
                encryptedImageString = _encryptionHelper.GetEncryptedString(imageStream);
            }

            return encryptedImageString;
        }
    }
}
