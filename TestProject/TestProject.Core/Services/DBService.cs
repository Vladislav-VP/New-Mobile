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
        public string CreateDatabase(string path)
        {
            var connection = new SQLiteAsyncConnection(path);
            string result;
            try
            {
                connection.CreateTablesAsync<UserModel, TodoItemModel>();
                result = "Database created";
            }
            catch(SQLiteException ex)
            {
                result = ex.Message;
            }
            finally
            {
                connection.CloseAsync();
                
            }

        return result;
        }
    }
}
