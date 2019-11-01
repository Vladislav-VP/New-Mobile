using System.Threading.Tasks;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IPhotoEditHelper
    {
        Task<byte[]> PickPhoto();

        Task<byte[]> TakePhoto();

        Task<byte[]> DeletePhoto();
    }
}
