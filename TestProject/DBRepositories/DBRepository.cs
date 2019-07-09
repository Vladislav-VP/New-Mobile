using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Repositories.Interfaces;
using SQLite;
using SQLiteNetExtensions;
using SQLitePCL;
using TestProject.Entity;
using TestProject.Configuration;

namespace TestProject.Repositories
{
    public class DBRepository : IDBRepository
    {
        private readonly string _path;

        public DBRepository()
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

        public Task<User> FindUser(object userPK)
        {
            var connection = new SQLiteAsyncConnection(_path);
            try
            {
                return connection.FindAsync<User>(userPK);
            }
            finally
            {
                connection.CloseAsync();
            }
        }

        public async Task AddUser(User user)
        {
            var connection = new SQLiteAsyncConnection(_path);
            try
            {
                if (await FindUser(user) == null)
                {
                    await connection.InsertAsync(user, typeof(User));
                }
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public Task<List<TodoItem>> GetTodoItemModels(User user)
        {
            throw new NotImplementedException();
        }
    }
}
