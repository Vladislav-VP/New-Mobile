using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject.Services.Interfaces
{
    public interface IBaseApiService
    {
        Task<T> Get<T>(string requestUri = null) where T : class;

        Task<TResponse> Post<TRequest, TResponse>(TRequest entity, string requestUri) where TResponse : class;

        Task Post(string requestUri);

        Task<T> Update<T>(T entity) where T : class;

        Task<T> Delete<T>(string requestUri = null) where T : class;
    }
}
