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
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly string _path;

        public GenericRepository()
        {
            string docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            _path = System.IO.Path.Combine(docsFolder, Constants.DatabaseName);
        }

        public async Task CreateDatabase()
        {
            var connection = new SQLiteAsyncConnection(_path);
            try
            {
                await connection.CreateTablesAsync<User, TodoItem>();
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public AsyncTableQuery<T> FindByPK<T>(Expression<Func<T, bool>> predicate) where T: class, new()
        {
            var connection = new SQLiteAsyncConnection(_path);
            return connection.Table<T>();
        }

        public Task<IEnumerable<T>> GetObjects()
        {
            throw new NotImplementedException();
        }

        public async Task<string> InsertObject(T obj)
        {
            var connection = new SQLiteAsyncConnection(_path);
            try
            {
                var result = await connection.InsertAsync(obj, obj.GetType());
                return string.Format("New {0} succesfully added", obj.GetType().Name);                
            }
            catch (SQLiteException)
            {
                return string.Format("This {0} already exists", obj.GetType().Name);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}
