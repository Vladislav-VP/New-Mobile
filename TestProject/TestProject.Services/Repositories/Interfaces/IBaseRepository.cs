using System.Collections.Generic;
using System.Threading.Tasks;

using TestProject.Entities;

namespace TestProject.Services.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class, new()
    {
        Task<bool> Insert(T obj);

        Task Update(T obj);

        Task Delete(T obj);

        Task<T> Find(object pk);

        Task<IEnumerable<T>> GetAllObjects();

        Task<T> FindWithQuery(string query);
    }
}
