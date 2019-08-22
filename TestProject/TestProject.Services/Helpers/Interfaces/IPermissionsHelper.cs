using System.Threading.Tasks;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IPermissionsHelper
    {
        Task<bool> TryRequestPermissions();
    }
}
