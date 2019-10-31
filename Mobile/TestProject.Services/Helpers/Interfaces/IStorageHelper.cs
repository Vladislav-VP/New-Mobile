using System.Threading.Tasks;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IStorageHelper<T>
    {
        Task Save(int id);

        Task<int> Get();

        void Clear();
    }
}
