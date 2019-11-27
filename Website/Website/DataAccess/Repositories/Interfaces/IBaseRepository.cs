using System.Linq;

using Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);

        void Delete(TEntity entity);

        TEntity Find(int id);
    }
}
