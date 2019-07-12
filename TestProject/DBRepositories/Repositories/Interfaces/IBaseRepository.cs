using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestProject.Entities;

namespace TestProject.Services.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity, new()
    {
        Task<bool> Insert(T obj);

        Task Update(T obj);

        Task Delete<T>(object pk);

        Task<T> Find<T>(object pk) where T : class, new();

        Task<IEnumerable<T>> GetAllObjects<T>() where T : class, new();
    }
}
