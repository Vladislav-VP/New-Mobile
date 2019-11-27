using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

using DataAccess.Context;
using Entities;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>  where TEntity : BaseEntity
    {
        protected DbSet<TEntity> _dbSet;
        protected TodoListContext _context;
        protected bool _disposed = false;

        public BaseRepository()
        {
            _context = new TodoListContext();
            _dbSet = _context.Set<TEntity>();
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            EntityEntry<TEntity> entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            TEntity entity = Find(id);
            Delete(entity);
        }

        public TEntity Find(int id)
        {
            TEntity entity = _dbSet.Find(id);
            return entity;
        }
    }
}
