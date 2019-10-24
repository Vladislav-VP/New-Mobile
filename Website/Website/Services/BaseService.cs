using DataAccess.Context;
using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly TodoListContext _context;
        protected readonly BaseRepository<TEntity> _baseRepository;

        public BaseService(TodoListContext context)
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

        }

        public virtual void Delete(int id)
        {
            _baseRepository.Delete(id);
        }
    }
}
