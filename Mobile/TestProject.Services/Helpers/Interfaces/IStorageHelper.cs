using System.Threading.Tasks;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IStorageHelper
    {
        Task Save(string id);

        Task<string> Get();

        void Clear();
    }
}
