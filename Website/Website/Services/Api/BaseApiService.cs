using DataAccess.Repositories;
using Services.Interfaces;

namespace Services.Api
{
    public class BaseApiService<TEntity> : IBaseApiService<TEntity> where TEntity : class
    {
        protected readonly BaseRepository<TEntity> _baseRepository;

        public TEntity FindById(int id)
        {
            TEntity entity = _baseRepository.FindById(id);
            return entity;
        }

        public void Insert(TEntity entity)
        {
            _baseRepository.Insert(entity);
        }

        public void Update(TEntity entity)
        {
            _baseRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _baseRepository.Delete(id);
        }
    }
}
