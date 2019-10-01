using System.Threading.Tasks;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IPhotoEditHelper
    {
        Task<string> PickPhoto();

        Task<string> TakePhoto();
    }
}
