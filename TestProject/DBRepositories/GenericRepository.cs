using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestProject.Configuration;
using TestProject.Entity;
using TestProject.Repositories.Interfaces;

namespace TestProject.Repositories
{
    public class GenericRepository<T> : BaseRepository, IGenericRepository<T> where T : class, new()
    {
        public GenericRepository()
            : base() { }

        public AsyncTableQuery<T> GetTable<T>() where T: class, new()
        {
            var connection = new SQLiteAsyncConnection(_path);
            return connection.Table<T>();
        }

        public async Task<IEnumerable<T>> GetAllObjects<T>() where T : class, new()
        {
            var connection = new SQLiteAsyncConnection(_path);
            return await connection.Table<T>().ToListAsync();
        }

        // Returns: true if item inserted succesfully, otherwise - false.
        public async Task<bool> Insert(T obj)
        {
            var connection = new SQLiteAsyncConnection(_path);
            try
            {
                var result = await connection.InsertAsync(obj, obj.GetType());
                return true;              
            }
            catch (SQLiteException)
            {
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public async Task Update(T obj)
        {
            var connection = new SQLiteAsyncConnection(_path);
            try
            {
                await connection.UpdateAsync(obj, obj.GetType());
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public async Task Delete<T>(object pk) where T : class, new()
        {
            var connection = new SQLiteAsyncConnection(_path);
            try
            {
                await connection.DeleteAsync<T>(pk);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public async Task<T> Find<T>(object pk) where T : class, new()
        {
            var connection = new SQLiteAsyncConnection(_path);
            try
            {
                return await connection.FindAsync<T>(pk);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}
