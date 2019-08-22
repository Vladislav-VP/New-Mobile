using SQLite;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TestProject.Configurations;
using TestProject.Entities;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        private static readonly string _path;

        static BaseRepository()
        {
            string docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            _path = System.IO.Path.Combine(docsFolder, Constants.DatabaseName);
            CreateDatabase();
        }

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

        public async Task<IEnumerable<T>> GetAllObjects()
        {
            var connection = new SQLiteAsyncConnection(_path);
            return await connection.Table<T>().ToListAsync();
        }

        public async Task Delete(T obj)
        {
            var connection = new SQLiteAsyncConnection(_path);
            try
            {
                await connection.DeleteAsync(obj);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public async Task<T> Find(object pk)
        {
            var connection = new SQLiteAsyncConnection(_path);
            T obj = default;

            obj = await connection.FindAsync<T>(pk: pk);               
            
            return obj;
        }

        public async Task<T> FindWithQuery(string query)
        {
            var connection = new SQLiteAsyncConnection(_path);
            T obj = default;
            try
            {
                obj = await connection.FindWithQueryAsync<T>(query);
            }
            catch (Exception)
            {
                return null;
            }

            return obj;
        }

        private static void CreateDatabase()
        {
            var connection = new SQLiteAsyncConnection(_path);
            try
            {
                connection.CreateTablesAsync<User, TodoItem>();
            }
            finally
            {
                connection.CloseAsync();
            }
        }
    }
}
