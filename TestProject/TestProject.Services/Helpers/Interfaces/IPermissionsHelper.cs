using System.Threading.Tasks;

using Plugin.Permissions.Abstractions;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IPermissionsHelper
    {
        Task<bool> IsPermissionGranted(Permission permission);
    }
}
