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

        AsyncTableQuery<T> FindByPK<T>(Expression<Func<T, bool>> predicate) where T : class, new();

        Task<string> InsertObject(T obj);

        Task<IEnumerable<T>> GetObjects();
    }
}
