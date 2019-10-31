using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject.Services.Interfaces
{
    public interface IBaseApiService<TEntity>
    {
        Task<IEnumerable<TEntity>> Get();

        Task<TEntity> Get(int id);

        Task<TEntity> Post(TEntity entity, string requestUri);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Delete(int id);
    }
}
