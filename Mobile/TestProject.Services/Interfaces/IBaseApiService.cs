using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject.Services.Interfaces
{
    public interface IBaseApiService<TEntity>
    {
        Task<IEnumerable<TEntity>> Get();

        Task<TEntity> Get(int id);

        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Delete(int id);
    }
}
