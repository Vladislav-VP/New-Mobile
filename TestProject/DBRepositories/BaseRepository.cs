using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Configuration;
using TestProject.Entity;
using TestProject.Repositories.Interfaces;

namespace TestProject.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly string _path;

        public BaseRepository()
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
    }
}
