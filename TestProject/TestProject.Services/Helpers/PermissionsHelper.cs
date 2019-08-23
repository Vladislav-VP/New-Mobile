using System.Collections.Generic;
using System.Threading.Tasks;

using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class PermissionsHelper : IPermissionsHelper
    {
        public async Task<bool> TryRequestPermission(Permission permission)
        {
            PermissionStatus status =
                await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            if (status != PermissionStatus.Granted)
            {
                status = await GetPermission(permission);
            }

            return status == PermissionStatus.Granted;
        }

        private async Task<PermissionStatus> GetPermission(Permission permission)
        {
            Dictionary<Permission, PermissionStatus> permissionsDictionary = await CrossPermissions
                    .Current
                    .RequestPermissionsAsync(permission);
            return permissionsDictionary[permission];
        }
    }
}
