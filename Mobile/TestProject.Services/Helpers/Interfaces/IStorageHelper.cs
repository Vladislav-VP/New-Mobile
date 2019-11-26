using System.Threading.Tasks;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IStorageHelper
    {
        Task Save(int id);

        Task<int> Get();

        void Clear();
    }
}
