using System.Threading.Tasks;

using TestProject.ApiModels.User;

namespace TestProject.Services.Interfaces
{
    public interface IBaseApiService
    {
        Task<T> Get<T>(string requestUri = null) where T : class;

        Task<TResponse> Post<TRequest, TResponse>(TRequest entity, string requestUri,
            bool isAuthorizationRequired = true) where TResponse : class;

        Task<T> Update<T>(T entity) where T : class;

        Task<T> Delete<T>(string requestUri = null) where T : class;

        Task<ResponseRefreshAccessTokenUserApiModel> RefreshToken();
    }
}
