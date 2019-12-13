using System.Threading.Tasks;

using Entities;
using ViewModels.Api;
using ViewModels.Api.User;

namespace Services.Interfaces
{
    public interface ISecurityService
    {
        TokenData GenerateTokens(User user);

        Task<ResponseRefreshAccessTokenUserApiView> RefreshToken(RequestRefreshAccessTokenUserApiView tokens);
    }
}
