using System.Collections.Generic;
using System.Threading.Tasks;

using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class PermissionsHelper : IPermissionsHelper
    {
        public async Task<bool> TryRequestPermissions()
        {
            await CrossMedia.Current.Initialize();
            PermissionStatus cameraStatus =
                await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            PermissionStatus storageStatus =
                await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                Dictionary<Permission, PermissionStatus> permissionsDictionary = await CrossPermissions
                    .Current
                    .RequestPermissionsAsync(Permission.Camera, Permission.Storage);
                cameraStatus = permissionsDictionary[Permission.Camera];
                storageStatus = permissionsDictionary[Permission.Storage];
            }

            return cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted;
        }
    }
}
