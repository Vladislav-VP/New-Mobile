using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject.Services.Interfaces
{
    public interface IBaseApiService
    {
        Task<T> Get<T>(string id, string requestUri = null) where T : class;

        Task<TResponse> Post<TRequest, TResponse>(TRequest entity, string requestUri) where TResponse : class;

        Task<T> Update<T>(T entity) where T : class;

        Task<T> Delete<T>(string id) where T : class;
    }
}
