using System.Collections.Generic;

using DataAccess.Context;
using Entities;
using Repositories;

namespace Services.Api
{
    public class BaseApiService<TEntity> where TEntity : BaseEntity
    {
        protected readonly TodoListContext _context;
        protected readonly BaseRepository<TEntity> _baseRepository;

        public BaseApiService(TodoListContext context)
        {
            _context = context;
            _baseRepository = new BaseRepository<TEntity>(_context);
        }

        public virtual TEntity FindById(int id)
        {
            TEntity entity = _baseRepository.Find(id);
            return entity;
        }

        public virtual IEnumerable<TEntity> GetAllObjects()
        {
            IEnumerable<TEntity> entities = _baseRepository.GetAllObjects();
            return entities;
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
