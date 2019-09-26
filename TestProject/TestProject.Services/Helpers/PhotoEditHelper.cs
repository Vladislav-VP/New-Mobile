﻿using System.IO;
using System.Threading.Tasks;

using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;

using TestProject.Services.Enums;
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

        public async Task<string> ReplacePhoto(EditPhotoDialogResult result)
        {
            MediaFile file = await GetMediaFile(result);

            if (file == null)
            {
                return null;
            }

            string encryptedImageString = null;
            using (Stream imageStream = file.GetStream())
            {
                encryptedImageString = _encryptionHelper.GetEncryptedString(imageStream);
            }

            return encryptedImageString;
        }
        
        private async Task<MediaFile> GetMediaFile(EditPhotoDialogResult result)
        {
            MediaFile file = null;

            switch (result)
            {
                case EditPhotoDialogResult.ChooseFromGallery:
                    file = await _photoCaptureHelper.PickPhoto();
                    break;
                case EditPhotoDialogResult.TakePicture:                    
                    bool isCameraPermitted = await _permissionsHelper.IsPermissionGranted(Permission.Camera);
                    bool isStoragePermitted = await _permissionsHelper.IsPermissionGranted(Permission.Storage);
                    if (isCameraPermitted && isStoragePermitted)
                    {
                        file = await _photoCaptureHelper.TakePhoto();
                    }
                    break;
                default:
                    break;
            }

            return file;
        }

    }
}
