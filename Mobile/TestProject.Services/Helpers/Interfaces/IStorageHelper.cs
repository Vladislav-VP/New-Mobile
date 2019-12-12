using System.Threading.Tasks;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IStorageHelper
    {
        Task Save(string key, string value);

        Task<string> Get(string key);

        void Clear();
    }
}
