using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Services.Interfaces;
using SQLite;
using SQLiteNetExtensions;
using SQLitePCL;
using TestProject.Core.Models;

namespace TestProject.Core.Services
{
    public class DBService : IDBService
    {
        private string _path;

        public DBService()
        {
            string docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            _path = System.IO.Path.Combine(docsFolder, Constants.DatabaseFile);
            //CreateDatabase();
        }
        public async Task<string> CreateDatabase()
        {
            var connection = new SQLiteAsyncConnection(_path);
            try
            {
                await connection.CreateTablesAsync<UserModel, TodoItemModel>();
                return "Database created";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
            finally
            {
                connection.CloseAsync();                
            }
        }

        public Task<UserModel> FindUser(object userPK)
        {
            var connection = new SQLiteAsyncConnection(_path);
            try
            {
                return connection.FindAsync<UserModel>(userPK);
            }
            finally
            {
                connection.CloseAsync();
            }
        }

        public async Task AddUser(UserModel user)
        {
            var connection = new SQLiteAsyncConnection(_path);
            try
            {
                if (await FindUser(user) == null)
                {
                    await connection.InsertAsync(user, typeof(UserModel));
                }
            }
            finally
            {
                connection.CloseAsync();
            }
        }

        public Task<List<TodoItemModel>> GetTodoItemModels(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
