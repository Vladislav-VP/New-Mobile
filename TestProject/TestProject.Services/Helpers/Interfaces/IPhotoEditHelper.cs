using System.Threading.Tasks;

using TestProject.Services.Enums;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IPhotoEditHelper
    {
        Task<string> ReplacePhoto(EditPhotoDialogResult result);
    }
}
