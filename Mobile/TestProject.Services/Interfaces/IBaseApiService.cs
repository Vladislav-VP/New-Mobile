using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject.Services.Interfaces
{
    public interface IBaseApiService<TEntity>
    {
        Task<IEnumerable<TEntity>> GetObjectsList(string requestUri = null);

        Task<TEntity> Get(int id, string requestUri = null);

        Task<TEntity> Post(TEntity entity, string requestUri);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Delete(int id);
    }
}
