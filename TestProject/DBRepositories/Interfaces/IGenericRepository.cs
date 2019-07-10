using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task CreateDatabase();

        AsyncTableQuery<T> GetTable<T>() where T : class, new();

        Task<string> Insert(T obj);

        Task<IEnumerable<T>> GetAllObjects<T>() where T : class, new();

        Task Update(T obj);

        Task Delete<T>(object pk) where T : class, new();

        Task<T> Find<T>(object pk) where T : class, new();
    }
}
