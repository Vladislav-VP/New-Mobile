using DataAccess.Repositories;
using Entities;

namespace Services.Api
{
    public class BaseApiService<TEntity> where TEntity : BaseEntity
    {
        protected readonly BaseRepository<TEntity> _baseRepository;

        public BaseApiService()
        {
            _baseRepository = new BaseRepository<TEntity>();
        }

        public virtual TEntity FindById(int id)
        {
            TEntity entity = _baseRepository.Find(id);
            return entity;
        }

        public virtual void Insert(TEntity entity)
        {
            _baseRepository.Insert(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _baseRepository.Update(entity);
        }

        public virtual void Delete(int id)
        {
            _baseRepository.Delete(id);
        }
    }
}
